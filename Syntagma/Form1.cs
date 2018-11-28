using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Syntagma
{
    public partial class Form1 : Form
    {
        public static bool treeLoaded = false;  // Has documentation been read?
        Form2 Initialise = new Form2();
        Form3 Document = new Form3();
        Form4 Parameters = new Form4();
        public static string inputFile = "Input.txt";
        public static string outputFile = "Logfile.txt";
        public static string nameFile = "Classnames.txt";
        public static string paramFile = "Parameters.txt";
        public static bool wordCapitalise = true;
        public static string sentenceEnd = ".?!";
        public static string wordEnds = ")]}':;,-\"";
        public static string wordStarts = "([{'\"";
        public static float Param1 = 5.0F;
        public static int Param2 = 2;
        public static int Param3 = 4;
        public static float Param4 = 20.0F;
        public static float Param5 = 2.0F;
        public static float Param6 = 1.0F;
        public static float Param7 = 5.0F;
        public static float Param8 = 0.02F;

        class Sentence
        {
            public List<int> words = new List<int>();    // List of words forming a sentence
        }

        public class Item       // Stores the attributes of a wordtype or word class
        {
            public string itName;    // Name corresponding to this item's index
            public int itCount = 0;   // Number of times this item has occurred in the corpus
            public List<int> itMembers = new List<int>(); // itMembers of this class
            public List<int> itClass = new List<int>(); // Classes of which this item is a member
            public Dictionary<int, int> itPre = new Dictionary<int, int>();   // Preceding items with frequencies
            public List<int> preSig = new List<int>();  // Items preceding significantly
            public Dictionary<int, int> itPost = new Dictionary<int, int>();  // Following items
            public List<int> postSig = new List<int>();  // Items following significantly
        }

        List<Sentence> sentenceList = new List<Sentence>();    // List of all sentenceList
        int totalWords = 0;                                  // Number of words in the inputCorpus
        int lastWordtype = 1;     // Number of word types, allow for full stop being '1'
        int totalItems;     // Number of word types plus classes
        int firstClass;
        int nCollocs;       // Number of collocations
        float fnCollocs;    // Floating point equivalent
        int nsigCollocs;    // Number of significant collocations
        int nSignif;          // Number of significant collocations
        float fnSignif;       // Floating point equivalent of the latter
        int numClasses = 0;   // Number of classes
        int nMultiple;      // No of words with multiple class memberships
        int prevTypescl = 0;
        int totalTypescl = 0;   // No of word types classified
        int currTypescl = 0;    // Wd types classified this phase
        int totalTokencl;   // No of word tokens classified
        List<Item> wdCl = new List<Item>();    // List of word types and classes
        // Translating a structure into its index within wdCl
        Dictionary<long, int> structIndex = new Dictionary<long, int>();
        // Potential structures waiting to be tested
        Dictionary<long, int> structWait = new Dictionary<long, int>();

        StreamWriter logFile;
        StreamReader inputCorpus;
        StreamReader clnameFile;
        Stopwatch tiMer = new Stopwatch();
        long overallTime = 0; // For Stopwatch elapsed time
        long phaseTime;     // Elapsed time for one phase
        int displayLevel, logLevel;
        int nPhase2 = 1;    // Number of times for Phase2's next run
        Dictionary<long, Dictionary<int, int>> conText // Groups of items bracketed
            = new Dictionary<long, Dictionary<int, int>>();  // between classes
        Dictionary<int, int> biggestDict    // Biggest list of items in conText
            = new Dictionary<int, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Setinitial_Click(object sender, EventArgs e)
        {
            Initialise.ShowDialog();
        }

        private void Helpbutton_Click(object sender, EventArgs e)
        {
            Document.ShowDialog();
        }

        private void Params_Click(object sender, EventArgs e)
        {
            Parameters.ShowDialog();
        }

        private void Phase1_Click(object sender, EventArgs e)
        {
            string line;
            if (System.IO.File.Exists(inputFile) != true)
            {
                MessageBox.Show("File " + inputFile + " cannot be found.");
                return;
            }
            tiMer.Restart();
            phaseTime = 0;
            Phase1Init();
            readCorpus();
            countCollocations();
            findSignificances();
            line = string.Format("{0} significant collocations", nsigCollocs);
            if (displayLevel >= 2) MessageBox.Show(line);
            if (logLevel >= 2)
            {
                logFile.WriteLine();
                logFile.WriteLine(line);
            }
            classifyWords();
            showClasses();
            logFile.WriteLine();
            logFile.WriteLine("Phase 1 has terminated: {0} words classified", currTypescl);
            MessageBox.Show(string.Format("Phase 1 has terminated: {0} words classified",
                currTypescl));
        }

        private void Phase1Init()
        {           // Perform initialisation before Phase 1 starts
            displayLevel = 1;
            logLevel = 1;
            Phase1.Enabled = false;
            Setinitial.Enabled = false;
            readParam.Enabled = false;
            displayLevel = Convert.ToInt32(LogDisplay.Text);
            logLevel = Convert.ToInt32(LogPrint.Text);
            logFile = new StreamWriter(outputFile);
            logFile.WriteLine("Syntagma - Syntax discovery program Vn. 1.1");
            logFile.WriteLine();
            logFile.WriteLine("Phase 1 is starting");
            if (logLevel == 0) return;
            logFile.WriteLine();
            logFile.WriteLine("Input file: " + inputFile);
            logFile.WriteLine("Logging file: " + outputFile);
            logFile.WriteLine("Sentence end characters: " + sentenceEnd);
            logFile.WriteLine("Word end characters: " + wordEnds);
            logFile.WriteLine("Word start characters: " + wordStarts);
            if (wordCapitalise) logFile.WriteLine("Words will be capitalised.");
            else logFile.WriteLine("Words will not be capitalised.");
            logFile.WriteLine("Parameter 1: {0:N1}", Param1);
            logFile.WriteLine("Parameter 2: {0}", Param2);
            logFile.WriteLine("Parameter 3: {0}", Param3);
            logFile.WriteLine("Parameter 4: {0:N1}", Param4);
            logFile.WriteLine("File logging level: {0}", logLevel);
        }

        public void readCorpus()
        {
            // Read in the inputCorpus of text, removing punctuation, 
            // converting words to unique integers and building sentenceList
            int s1, wordIndexW;
            string word, line, wordKeep;
            bool endofSentence = false;
            int nsentenceList = 0;
            Sentence newSentence = new Sentence();
            // Temporary dictionary for translating words to integers
            Dictionary<string, int> wordToInt = new Dictionary<string, int>();
            inputCorpus = new StreamReader(inputFile);
            char[] charStarts = wordStarts.ToCharArray();
            char[] charEnds = (wordEnds + sentenceEnd).ToCharArray();
            char[] charSent = sentenceEnd.ToCharArray();

            wdCl.Add(new Item());           // Item zero is not used
            newSentence.words.Add(1);       // Start sentence with full stop
            wdCl.Add(new Item());
            wdCl[1].itName = ".";           // Full stop has index 1
            while ((line = inputCorpus.ReadLine()) != null)
            {
                if (wordCapitalise) line = line.ToUpper();
                line.Trim();    // Ensure line ends in only one blank
                line = line + " ";
                while (line != "")
                {   // Remove starting punctuation
                    line = line.TrimStart(charStarts);
                    s1 = line.IndexOf(" ");
                    word = line.Substring(0, s1);       // Get next word
                    line = line.Remove(0, s1 + 1);
                    wordKeep = word;
                    word = word.TrimEnd(charEnds);  // Trim off all punctuation
                    if (word == "") continue;
                    if ((wordKeep != word) &&
                        (wordKeep.LastIndexOfAny(charSent, word.Length) > 0))
                        endofSentence = true;   // Has end of sentence beeen trimmed?
                    ++totalWords;
                    if (!wordToInt.TryGetValue(word, out wordIndexW))
                    {
                        ++lastWordtype;
                        wordToInt.Add(word, lastWordtype);
                        wordIndexW = lastWordtype;
                        wdCl.Add(new Item());   // Details of this new word type
                        wdCl[lastWordtype].itName = word;
                    }
                    newSentence.words.Add(wordIndexW);
                    if (endofSentence)
                    {
                        newSentence.words.Add(1);   // Terminate sentence with full stop
                        sentenceList.Add(newSentence);
                        ++nsentenceList;
                        newSentence = new Sentence();
                        newSentence.words.Add(1);   // Start next sentence with full stop
                    }
                    endofSentence = false;
                }
            }
            inputCorpus.Close();
            totalItems = lastWordtype;
            logFile.WriteLine();
            line = string.Format("Corpus has {0} words, {1} word types, {2} sentences",
                totalWords, lastWordtype, nsentenceList);
            firstClass = lastWordtype + 1;
            logFile.WriteLine(line);
            if (displayLevel >= 2) MessageBox.Show(line);
        }

        public void countCollocations()
        {   // This scans through all the sentenceList and compiles collocations
            // in the Pre and Post dictionaries for each word 
            int sentlength, i;
            Dictionary<int, int> adjDict;

            nCollocs = 0;       // Number of collocations
            foreach (Sentence sent in sentenceList)
            {
                sentlength = sent.words.Count;
                for (i = 0; i < sentlength; ++i)    // Count of words
                    ++wdCl[sent.words[i]].itCount;
                for (i = 1; i < sentlength; ++i)
                {
                    ++nCollocs;
                    adjDict = wdCl[sent.words[i]].itPre;
// Increment count for preceding item
                    increMent(ref adjDict, sent.words[i - 1]);
                    adjDict = wdCl[sent.words[i - 1]].itPost;
// Increment count for following item
                    increMent(ref adjDict, sent.words[i]);
                }
            }
            fnCollocs = nCollocs;
        }

        public void findSignificances()
        {   // Find significant collocations for all words
            int intWord;
            nSignif = 0;                // Number of significant collocations
            fnCollocs = nCollocs;
            for (intWord = 1; intWord <= lastWordtype; ++intWord)
                itemSignifs(intWord);
            fnSignif = (float)nSignif;
        }

        public void itemSignifs(int itm)
        {   // Find significant collocations for an item
            int mainCount, secondCount, secondWord, pairOverlap;
            float signif;
            mainCount = wdCl[itm].itCount;
            if (mainCount < Param2) return;

            foreach (KeyValuePair<int, int> pair in wdCl[itm].itPre)
            {
                pairOverlap = pair.Value;
                if (pairOverlap < Param2) continue;
                secondWord = pair.Key;
                secondCount = wdCl[secondWord].itCount;
                if (secondCount < Param2) continue;
                signif = signifLevel(mainCount, secondCount, pairOverlap, fnCollocs);
                if (signif >= Param1)
                {
                    if (logLevel >= 4)
                        logFile.WriteLine
                            ("{0,6:N2} st. devs.: {1} (freq. {2})" +
                            "links with {3} ({4}) overlap {5}",
                            signif, wdCl[secondWord].itName, secondCount,
                            wdCl[itm].itName, mainCount,
                            pairOverlap);
                    if (!wdCl[itm].preSig.Contains(secondWord))
                        wdCl[itm].preSig.Add(secondWord);
                    ++nsigCollocs;
                }
            }
            foreach (KeyValuePair<int, int> pair in wdCl[itm].itPost)
            {
                pairOverlap = pair.Value;
                if (pairOverlap < Param2) continue;
                secondWord = pair.Key;
                secondCount = wdCl[secondWord].itCount;
                if (secondCount < Param2) continue;
                signif = signifLevel(mainCount, secondCount, pairOverlap, fnCollocs);
                if (signif >= Param1)
                {
                    if (logLevel >= 4)
                        logFile.WriteLine
                            ("{0,6:N2} st. devs.: {1} (freq. {2})" +
                            "links with {3} ({4}) overlap {5}",
                            signif, wdCl[itm].itName, mainCount,
                            wdCl[secondWord].itName, secondCount,
                            pairOverlap);
                    if (!wdCl[itm].postSig.Contains(secondWord))
                        wdCl[itm].postSig.Add(secondWord);
                    ++nsigCollocs;
                }
            }
        }

        public float signifLevel(int freq1, int freq2, int overlap, float fBase)
        {               // Returns no. of st. devs. significance collocating two items
            float variance, fOverlap;
            fOverlap = overlap;
            variance = (float)freq1 * (float)freq2 / fBase;
            float std = (fOverlap - variance) / (float)Math.Sqrt(variance);
            return std;
        }

        public void classifyWords()  // Find words classifying together
        {
            int iWord, jWord, jComp, jStart;
            Item it1, it2;
            bool paired = false;
            for (iWord = 1; iWord <= lastWordtype; ++iWord)
            {   // Test whether word belongs in an existing class
                it1 = wdCl[iWord];
                if (it1.preSig.Count < Param3) continue;
                if (it1.postSig.Count < Param3) continue;
                if (it1.itClass.Count != 0) continue;
                jStart = firstClass;
                if (jStart <= iWord) jStart = iWord + 1; // Only compare i with items > i
                for (jComp = jStart; jComp <= totalItems; ++jComp)
                {   // Does item iWord belong with existing class?
                    if (typeItem(jComp) != 2) continue; // Is jComp a class?
                    it2 = wdCl[jComp];
                    if (it2.preSig.Count < Param3) continue;
                    if (it2.postSig.Count < Param3) continue;
                    if (it2.itMembers.Contains(iWord)) continue;
                    paired = testPair(iWord, jComp);
                    if (paired) addWdToClass(jComp, iWord);
                }
                for (jWord = iWord + 1; jWord <= lastWordtype; ++jWord)
                {   // Will iWord and jWord form a new class?
                    if (it1.itClass.Count > 0) break;
                    it2 = wdCl[jWord];
                    if (it2.preSig.Count < Param3) continue;
                    if (it2.postSig.Count < Param3) continue;
                    paired = testPair(iWord, jWord);
                    if (paired) createClass(iWord, jWord);
                }
            }
        }

        public void createClass(int wd1, int wd2)
        {
            Item newClass = new Item();
            Item it1, it2;
            it1 = wdCl[wd1];
            it2 = wdCl[wd2];
            ++totalItems;
            wdCl.Add(newClass);
            ++numClasses;
            newClass.itName = "Class" + string.Format("{0}", numClasses);
            newClass.itMembers.Add(wd1);
            newClass.itMembers.Add(wd2);
            it1.itClass.Add(totalItems);
            it2.itClass.Add(totalItems);
            if (logLevel >= 2)
            {
                logFile.WriteLine("Class created from {0} and {1}",
                    it1.itName, it2.itName);
            }
            if (displayLevel >= 2)
                MessageBox.Show(string.Format("Class created from {0} and {1}",
                    it1.itName, it2.itName));
            newClass.itCount = it1.itCount + it2.itCount;
            newClass.itPre = mergeDict(ref it1.itPre, ref it2.itPre);
            newClass.itPost = mergeDict(ref it1.itPost, ref it2.itPost);
            nameClasses();
            itemSignifs(totalItems);
            checkClass();
        }

        public bool testPair(int indx1, int indx2)
        {   // Checks whether items are to be classified
            Item itm1 = wdCl[indx1];
            Item itm2 = wdCl[indx2];
            int preCommon, postCommon, preCent, postCent;
            preCommon = inCommon(ref itm1.preSig, ref itm2.preSig);
            if (preCommon < Param3) return false;
            postCommon = inCommon(ref itm1.postSig, ref itm2.postSig);
            if (postCommon < Param3) return false;
            if (logLevel >= 2)
            {
                logFile.WriteLine();
                logFile.WriteLine("{0} joining {1}",
                    itm1.itName, itm2.itName);
            }
            if (logLevel >= 3)
            {
                preCent = (preCommon * 100) / Math.Min(itm1.preSig.Count, itm2.preSig.Count);
                postCent = (postCommon * 100) / Math.Min(itm1.postSig.Count, itm2.postSig.Count);
                logFile.WriteLine("Significant pre counts: {0} {1}; {2} {3}; " +
                    "overlap {4}",
                    itm1.itName, itm1.preSig.Count,
                    itm2.itName, itm2.preSig.Count,
                    preCommon);
                logFile.WriteLine("Significant post counts: {0} {1}; {2} {3}; " +
                    "overlap {4}",
                    itm1.itName, itm1.postSig.Count,
                    itm2.itName, itm2.postSig.Count,
                    postCommon);
            }
            return true;
        }

        public void checkClass()
        {   // Checks latest class for merging with any lower one
            string className = wdCl[totalItems].itName;
            List<int> newList = new List<int>();
            int i, lastpreCount, thispreCount;
            float prePC, postPC;
            int lastpostCount, thispostCount, overCount;
            for (i = firstClass; i < totalItems; ++i)
            {
                if (typeItem(i) != 2) continue; // Check for class
                newList = overlapList(ref wdCl[i].preSig, ref wdCl[totalItems].preSig);
                lastpreCount = listCount(ref wdCl[totalItems].preSig);
                overCount = listCount(ref newList);
                thispreCount = listCount(ref wdCl[i].preSig);
                thispreCount = Math.Min(thispreCount, lastpreCount);
                if (thispreCount == 0) continue;
                prePC = (overCount * 100) / thispreCount;
                if (prePC < Param4) continue;   // Preceding contexts do not overlap sufficiently
                newList = overlapList(ref wdCl[i].postSig, ref wdCl[totalItems].postSig);
                lastpostCount = listCount(ref wdCl[totalItems].postSig);
                overCount = listCount(ref newList);
                thispostCount = listCount(ref wdCl[i].postSig);
                thispostCount = Math.Min(thispostCount, lastpostCount);
                if (thispostCount == 0) continue;
                postPC = (overCount * 100) / thispostCount;
                if (postPC < Param4) continue;   // Following contexts do not overlap sufficiently
                if (logLevel >= 2)
                {
                    logFile.WriteLine();
                    logFile.WriteLine("{0} joining with the new class, " +
                        "overlap pre {1}%, post {2}%",
                        wdCl[i].itName, prePC, postPC);
                }
                if (displayLevel >= 2)
                    MessageBox.Show(string.Format("{0} joining with the new class, " +
                        "overlap pre {1}%, post {2}%",
                        wdCl[i].itName, prePC, postPC));
                uniteClass(i);
                return;
            }
        }

        public void addWdToClass(int classId, int word)
        {
            Item classItem, wordItem;
            wordItem = wdCl[word];
            classItem = wdCl[classId];
            if (classItem.itMembers.Contains(word)) return;
            if (!wordItem.itClass.Contains(classId)) wordItem.itClass.Add(classId);
            if (!classItem.itMembers.Contains(word)) classItem.itMembers.Add(word);
            classItem.itCount = classItem.itCount + wordItem.itCount;
            classItem.itPre = mergeDict(ref classItem.itPre, ref wordItem.itPre);
            classItem.itPost = mergeDict(ref classItem.itPost, ref wordItem.itPost);
            itemSignifs(classId);
        }

        public void uniteClass(int classId)
        {   // Unites the uppermost class with the one specified
            Item tgtCl = wdCl[classId];
            Item fromCl = wdCl[totalItems];
            tgtCl.itCount = tgtCl.itCount + fromCl.itCount;
            tgtCl.itMembers = mergeList(ref tgtCl.itMembers, ref fromCl.itMembers);
            foreach (int memBer in fromCl.itMembers)
            {   // Remove class membership for totalItems and add for classId
                wdCl[memBer].itClass.Remove(totalItems);
                if (!wdCl[memBer].itClass.Contains(classId))
                    wdCl[memBer].itClass.Add(classId);
                if (logLevel >= 3)
                {
                    logFile.WriteLine("Uniting classes {0} and {1}, linking {2}",
                        fromCl.itName, tgtCl.itName, wdCl[memBer].itName);
                }
                if (!tgtCl.itMembers.Contains(memBer)) tgtCl.itMembers.Add(memBer);
            }
            tgtCl.itPre = mergeDict(ref tgtCl.itPre, ref fromCl.itPre);
            tgtCl.itPost = mergeDict(ref tgtCl.itPost, ref fromCl.itPost);
            tgtCl.preSig = mergeList(ref tgtCl.preSig, ref fromCl.preSig);
            tgtCl.postSig = mergeList(ref tgtCl.postSig, ref fromCl.postSig);
            nullClass(totalItems);
        }

        private void Phase2_Click(object sender, EventArgs e)
        {
            tiMer.Restart();
            phaseTime = 0;
            Phase2Init();
            parseText();
            findStructs();
            gleanContexts();
            selectContext();
            assimilateClass();
            classifyWords();
            showClasses();
            logFile.WriteLine();
            logFile.WriteLine("Phase 2 has terminated: {0} more words classified",
                currTypescl);
            MessageBox.Show(string.Format("Phase 2 has terminated: {0} more words classified",
                currTypescl));
            ++nPhase2;
            Phase2.Text = string.Format("Run Phase 2: (may be repeated); run no. {0}",
                nPhase2);
        }

        private void Phase2Init()
        {           // Perform initialisation before Phase 2 starts
            displayLevel = Convert.ToInt32(LogDisplay.Text);
            logLevel = Convert.ToInt32(LogPrint.Text);

            logFile.WriteLine();
            logFile.WriteLine("Phase 2 is starting - run {0}", nPhase2);
            if (logLevel == 0) return;
            logFile.WriteLine();
            logFile.WriteLine("Parameter 1: {0:N1}", Param1);
            logFile.WriteLine("Parameter 2: {0}", Param2);
            logFile.WriteLine("Parameter 3: {0}", Param3);
            logFile.WriteLine("Parameter 4: {0:N1}", Param4);
            logFile.WriteLine("Parameter 5: {0:N1}", Param5);
            logFile.WriteLine("Parameter 6: {0:N1}", Param6);
            logFile.WriteLine("Parameter 7: {0:N1}", Param7);
            logFile.WriteLine("Parameter 8: {0:N2}", Param8);
            logFile.WriteLine("File logging level: {0}", logLevel);
        }

        public void parseText()  // Collocate words with classes and structures
        {
            int i, wd1, cl1, cl2, dupPos, classPos;
            int sentLength, st1, val;
            long classPair;
            bool inStruct;
            Sentence classSent = new Sentence();  // Copy with classes
            Sentence dupSent = new Sentence();  // Copy after all parsing

            conText.Clear();
            for (i = firstClass; i <= totalItems; ++i)
                wdCl[i].itCount = 0;

// Parse all sentences
            foreach (Sentence sent in sentenceList)
            {
                classSent.words.Clear();  // Sentence with class membership
                dupSent.words.Clear();
                sentLength = sent.words.Count;
                for (i = 0; i < sentLength; ++i)
                {   // Copy input resolving class membership
                    wd1 = sent.words[i];    // Member of one class?                  
                    if (wdCl[wd1].itClass.Count == 1)
                    {
                        cl1 = wdCl[wd1].itClass[0];
                        classSent.words.Add(cl1);
                        wdCl[cl1].itCount += 1;
                    }
                    else classSent.words.Add(wd1);
                }

                classPos = -1;
                do
                {   // Resolve structures and classes thereof
                    ++classPos;
                    if (classPos > sentLength - 1) break;
                    i = classSent.words[classPos];
                    dupSent.words.Add(i);
                    dupPos = dupSent.words.Count - 1;
                    if (typeItem(i) != 2) continue; // Present item a class?
                    inStruct = true;
                    while (inStruct)
                    {
                        if (classPos > sentLength - 1) break;
                        cl1 = dupSent.words[dupPos];
                        cl2 = classSent.words[classPos + 1];
                        if (typeItem(cl2) < 2) break;
                        classPair = makeLong(cl1, cl2);
                        if (structIndex.TryGetValue(classPair, out st1))
                        {
                            dupSent.words[dupPos] = st1;
                            wdCl[st1].itCount += 1;
                            ++classPos;
                            if (wdCl[st1].itClass.Count == 1)
                            {   // Structure is a class member
                                cl1 = wdCl[st1].itClass[0];
                                dupSent.words[dupPos] = cl1;
                                wdCl[cl1].itCount += 1;
                            }
                        }
                        else inStruct = false;
                    }
                } while (classPos < sentLength);

                sentLength = dupSent.words.Count;

                for (i = 1; i < sentLength - 2; ++i)
                {   // Find items between composites
                    if ((dupSent.words[i - 1] > lastWordtype)
                        && (dupSent.words[i + 1] > lastWordtype))
                    {
                        groupWord(dupSent.words[i - 1], dupSent.words[i + 1],
                            dupSent.words[i]); // Item between composites
                    }
                }
                for (i = 1; i < sentLength - 2; ++i)
                {   // Test for classes together forming a structure
                    if ((dupSent.words[i] > lastWordtype)
                        && (dupSent.words[i + 1] > lastWordtype))
                    {
                        classPair = makeLong(dupSent.words[i], dupSent.words[i + 1]);
                        if (!structIndex.ContainsKey(classPair))
                        {
                            if (structWait.TryGetValue(classPair, out val))
                                structWait[classPair] = ++val;
                            else structWait.Add(classPair, 1);
                        }
                    }
                }
            }

        }

        public void findStructs()
        {   // Examine everything found within contexts
            int wd1, wd2, countWd1, countWd2;
            float pC1, pC2;
            foreach (KeyValuePair<long, int> pair in structWait)
            {
                wd1 = getUpper(pair.Key);
                wd2 = getLower(pair.Key);
                countWd1 = wdCl[wd1].itCount;
                countWd2 = wdCl[wd2].itCount;
                pC1 = ((float)pair.Value * 100.0F) / (float)countWd1;
                if (pC1 < Param7) continue;
                pC2 = ((float)pair.Value * 100.0F) / (float)countWd2;
                if (pC2 < Param7) continue;
                if (structIndex.ContainsKey(pair.Key)) continue;
                Item newItem = new Item();
                newItem.itName = "(" + wdCl[wd1].itName + "+" + wdCl[wd2].itName + ")";
                newItem.itMembers.Add(-1);
                newItem.itCount = pair.Value;
                wdCl.Add(newItem);
                ++totalItems;
                structIndex.Add(pair.Key, totalItems);
                if (logLevel >= 2)
                {
                    logFile.WriteLine();
                    logFile.WriteLine("New {0} from {1} ({2:0.0}%) and " +
                        "{3} ({4:0.0}%)",
                        newItem.itName, wdCl[wd1].itName, pC1,
                        wdCl[wd2].itName, pC2);
                }
            }
            structWait.Clear();
        }

        public int findClass(int iWord)
        {   // Translates an item into the class of which it is a member
            int nClasses;
            nClasses = wdCl[iWord].itClass.Count;  // Mamber of how many classes?
            if (nClasses == 0) return iWord;    // Not a member of a class
            if (nClasses > 1) return iWord;     // Multiple class membership 
            return wdCl[iWord].itClass[0];
        }

        public int typeItem(int indEx)
        {   // Returns type of item (0=nullified; 1=word; 2=class; 3=structure)
            Item iItem;
            if (indEx <= lastWordtype) return 1;
            iItem = wdCl[indEx];
            if (iItem.itName == ".Null.") return 0;
            if (iItem.itMembers.Count == 0) return 2;
            if (iItem.itMembers[0] == -1) return 3;
            return 2;
        }

        public void groupWord(int class1, int class2, int word)
        {   // Record an item bracketed by two classes or structures
            Dictionary<int, int> bitClass;
            long groupIng = makeLong(class1, class2);
            if (!conText.TryGetValue(groupIng, out bitClass))
            {
                bitClass = new Dictionary<int, int>();
                conText.Add(groupIng, bitClass);
            }
// Record frequency with which item appears between the two composites
            if (bitClass.ContainsKey(word))
                bitClass[word] = ++bitClass[word];
            else bitClass.Add(word, 1);
        }

        public long makeLong(int wd1, int wd2)
        {   // Packs two integers into a 64-bit long
            return ((long)wd1 << 32) + (long)wd2;
        }

        public int getUpper(long packedItem)
        {   // Get upper item of a pair packed into a long
            return (int)(packedItem >> 32);
        }

        public int getLower(long packedItem)
        {   // Get lower item of a pair packed into a long
            return (int)(packedItem & 0xFFFFFFFF);
        }

        public void gleanContexts()
        {   // Classify words sharing with one class in a context
            int class1, class2, middleClass, classTotal, wordTotal;
            int middleItem, numClass;
            float classPercent = 0.0F, wordPercent;
            Dictionary<int, int> middleDict;
            foreach (KeyValuePair<long, Dictionary<int, int>> pair in conText)
            {
                class1 = getUpper(pair.Key);
                class2 = getLower(pair.Key);
                middleDict = pair.Value;
                middleClass = 0;
                numClass = 0;
                foreach (KeyValuePair<int, int> inContext in middleDict)
                {
                    middleItem = inContext.Key;
                    if (typeItem(middleItem) == 2)  // A class?
                    {
                        classTotal = inContext.Value;
                        classPercent =
                            (float)(inContext.Value * 100) / (float)wdCl[middleItem].itCount;
                        if (classPercent >= Param5)
                        {
                            middleClass = middleItem;
                            ++numClass;
                        }
                    }
                }
                if (numClass != 1) continue;
                if (logLevel >= 2)
                {
                    logFile.WriteLine();
                    logFile.WriteLine("{0} found between {1} and {2} ({3:0.0}%):",
                        wdCl[middleClass].itName, wdCl[class1].itName,
                        wdCl[class2].itName, classPercent);
                }
                foreach (KeyValuePair<int, int> inContext in middleDict)
                {
                    middleItem = inContext.Key;
                    if (typeItem(middleItem) == 2) continue;
                    wordTotal = inContext.Value;
                    // Find amount of time spent in this context
                    wordPercent =
                        (float)(wordTotal * 100) /
                            (float)wdCl[middleItem].itCount;
                    if (wordPercent < Param6) continue;
                    if (logLevel >= 3)
                    {
                        logFile.WriteLine("{0} ({1} ocurrences) occurs between " +
                            "{2} and {3} {4} times ({5:0.0}%)",
                            wdCl[middleItem].itName, wdCl[middleItem].itCount,
                            wdCl[class1].itName, wdCl[class2].itName,
                            wordTotal, wordPercent);
                    }
                    if (testPair(middleClass, middleItem))
                        addWdToClass(middleClass, middleItem);
                }
            }
        }

        public void selectContext()
        {   // Find the biggest group within class contexts
            int class1, class2, classTotal;
            Dictionary<int, int> middleDict;
            biggestDict.Clear();
            int biggestCount = 0;
            int keptClass1 = 0, keptClass2 = 0;
            foreach (KeyValuePair<long, Dictionary<int, int>> pair in conText)
            {
                class1 = getUpper(pair.Key);
                class2 = getLower(pair.Key);
                middleDict = pair.Value;
                classTotal = middleDict.Count;
                if (classTotal > biggestCount)
                {
                    biggestCount = classTotal;
                    biggestDict = middleDict;
                    keptClass1 = class1;
                    keptClass2 = class2;
                }
            }
            if (logLevel >= 2)
            {
                logFile.WriteLine();
                logFile.WriteLine("Words between {0} and {1} to form new class",
                    wdCl[keptClass1].itName, wdCl[keptClass2].itName);
            }
            if (displayLevel >= 2)
                MessageBox.Show(string.Format("Words between {0} and {1} to form new class",
                    wdCl[keptClass1].itName, wdCl[keptClass2].itName));
        }

        public void assimilateClass()
        {   // Makes a class out of biggestDict
            Item newClass = new Item();
            int thisWord, thisCount, wordTotal;
            float perCent;
            ++totalItems;
            wdCl.Add(newClass);
            ++numClasses;
            newClass.itName = "Class" + string.Format("{0}", numClasses);
            foreach (KeyValuePair<int, int> pair in biggestDict)
            {
                thisWord = pair.Key;
                if (typeItem(thisWord) == 2) continue;  // Is it a class?
                thisCount = pair.Value;
                wordTotal = wdCl[thisWord].itCount;
                perCent = (float)thisCount / (float)wordTotal;
                if (perCent > Param8)
                {
                    if (logLevel >= 3)
                    {
                        logFile.WriteLine("{0} added to new class ({1:N2}%)",
                            wdCl[thisWord].itName, perCent);
                    }
                    addWdToClass(totalItems, thisWord);
                }
            }
            nameClasses();
            itemSignifs(totalItems);
            checkClass();
        }

        public void nameClasses()   // Give classes user's names
        {
            int i;
            string line;
            if (!System.IO.File.Exists(nameFile)) return;
            clnameFile = new StreamReader(nameFile);
            for (i = firstClass; i <= totalItems; ++i)
            {
                if (typeItem(i) != 2) continue;
                if ((line = clnameFile.ReadLine()) == null) return;
                if (line == "") return;
                wdCl[i].itName = line;
            }
        }

        public void showClasses()  // Print the list of current classes
        {
            int i, i1, i2, wd1, wd2, count, structTotal = 0;
            float pC1, pC2;
            string line;
            int itType;
            nMultiple = 0;  // Number of words with multiple class membership
            nameClasses();
            numClasses = 0;
            prevTypescl = totalTypescl; // Number of word types classified
            totalTypescl = 0;
            totalTokencl = 0;
            logFile.WriteLine();
            for (i = 2; i <= lastWordtype; ++i)
            {
                if (wdCl[i].itClass.Count > 0)
                {
                    ++totalTypescl;
                    totalTokencl += wdCl[i].itCount;
                }
                if (wdCl[i].itClass.Count > 1) ++nMultiple;
            }
            currTypescl = totalTypescl - prevTypescl;
            for (i = firstClass; i <= totalItems; ++i)
                if (typeItem(i) == 2) ++numClasses; // Count classes
            if (logLevel == 0)
            {
                LogPrint.Text = "1";
                logLevel = 1;
                return;
            }
            for (i = firstClass; i <= totalItems; ++i)
                if (typeItem(i) == 2) showClass(i);
            logFile.WriteLine();
            if (structIndex.Count > 0)
            {
                foreach (KeyValuePair<long, int> pair in structIndex)
                {
                    wd1 = getUpper(pair.Key);
                    wd2 = getLower(pair.Key);
                    i1 = wdCl[wd1].itCount;
                    i2 = wdCl[wd2].itCount;
                    count = wdCl[pair.Value].itCount;
                    if (count == 0) continue;
                    structTotal += count;
                    pC1 = ((float)count * 100.0F) / (float)i1;
                    pC2 = ((float)count * 100.0F) / (float)i2;
                    logFile.WriteLine("{0} {1} times; " +
                        "{2,0:N1}% of {3} ({4}); " +
                        "{5,0:N1}% of {6} ({7})",
                        wdCl[pair.Value].itName, count,
                        pC1, wdCl[wd1].itName, i1,
                        pC2, wdCl[wd2].itName, i2);
                }
            }
            logFile.WriteLine();
            line = "{0} classes of {1} wd tokens, {2} wd types, ({3} this phase), " +
                "{4} structures, {5}% of all words";
            logFile.WriteLine(line, numClasses,
                totalTokencl, totalTypescl, currTypescl, 
                structIndex.Count, (totalTokencl * 100) / totalWords);
            if (displayLevel > 1)
                MessageBox.Show(string.Format(line, numClasses,
                    totalTokencl, totalTypescl, totalTypescl-prevTypescl, 
                    structIndex.Count, (totalTokencl * 100) / totalWords));
            prevTypescl = totalTypescl;
            logFile.WriteLine();
            phaseTime = tiMer.ElapsedMilliseconds;
            overallTime += phaseTime;
            logFile.WriteLine("Time taken for this phase: {0} ms.; overall time {1} ms.",
                phaseTime, overallTime);
            //            logFile.WriteLine();
            if (nMultiple == 0) return;
            logFile.WriteLine();
            line = "{0} words have multiple class membership";
            logFile.WriteLine(line, nMultiple);
            if (displayLevel > 1)
                MessageBox.Show(string.Format(line, nMultiple));
            for (i = 2; i <= lastWordtype; ++i)
            {
                itType = typeItem(i);
                if (itType == 0) continue;
                if (wdCl[i].itClass.Count > 1)
                {
                    logFile.WriteLine();
                    logFile.WriteLine("{0} is a member of the following {1} classes:",
                        wdCl[i].itName, wdCl[i].itClass.Count);
                    foreach (int clAss in wdCl[i].itClass)
                    {
                        logFile.WriteLine("   {0}", wdCl[clAss].itName);
                    }
                }
            }
        }

        public void showClass(int i)  // Print a current class
        {
            int wordTotal;
            List<int> classMembers;
            classMembers = wdCl[i].itMembers;
            if (classMembers.Count == 0) return;
            wordTotal = wdCl[i].itCount;
            logFile.WriteLine();
            logFile.WriteLine("{0} ({1} members) ({2} / {3} occurrences {4,6:N2}%):",
                wdCl[i].itName, classMembers.Count, wordTotal, totalWords,
                ((float)wordTotal*100F) / (float)totalWords);
            printList(ref classMembers);
            if (logLevel >= 4)
            {
                logFile.WriteLine("Preceding significant collocations:");
                printList(ref wdCl[i].preSig);
                logFile.WriteLine("Following significant collocations:");
                printList(ref wdCl[i].postSig);
            }
        }

        public void voidClass(int i)
        {   // Makes a class inoperative
            int wordTotal;
            Item thisClass = wdCl[i];
            List<int> classList;
            List<int> classMembers = thisClass.itMembers;
            if (logLevel >= 2)
            {
                float proPortion;
                logFile.WriteLine("Removing class:");
                wordTotal = 0;
                foreach (int memBer in classMembers)
                    wordTotal += wdCl[memBer].itCount;
                proPortion = ((float)wordTotal) / (float)totalWords;
                logFile.WriteLine("{0} ({1} members) ({2} occurrences /{3} {4:0.0%}):",
                    wdCl[totalItems].itName, classMembers.Count,
                    wordTotal, totalWords, proPortion);
                printList(ref classMembers);
                logFile.WriteLine();
            }
            foreach (int memBer in classMembers)
            {   // Detach members of this class
                classList = wdCl[memBer].itClass;
                i = classList.IndexOf(totalItems);
                classList.Remove(i);
            }
            nullClass(i);
        }

        public void nullClass(int i)
        {   // Nullifies a class
            Item thisClass = wdCl[i];
            thisClass.itName = ".Null.";
            thisClass.itCount = 0;
            thisClass.itMembers.Clear();
        }

        public void increMent(ref Dictionary<int, int> inDict, int place)
        {   // Increment the count at a particular place in a dictionary
            int p;
            if (inDict.TryGetValue(place, out p)) inDict[place] = ++p;
            else inDict.Add(place, 1);
        }

        public List<int> mergeList(ref List<int> List1, ref List<int> List2)
        {
            List<int> newList = new List<int>();
            foreach (int i in List1) newList.Add(i);
            foreach (int i in List2) if (!List2.Contains(i)) newList.Add(i);
            return newList;
        }

        public Dictionary<int, int> mergeDict(ref Dictionary<int, int> Dict1,
            ref Dictionary<int, int> Dict2)
        {
            Dictionary<int, int> newDict = new Dictionary<int, int>();
            int i = 0;
            foreach (KeyValuePair<int, int> pair in Dict1)
                newDict.Add(pair.Key, pair.Value);
            foreach (KeyValuePair<int, int> pair in Dict2)
            {
                if (Dict1.TryGetValue(pair.Key, out i))
                    newDict[pair.Key] = pair.Value + i;
                else newDict.Add(pair.Key, pair.Value);
            }
            return newDict;
        }

        public List<int> overlapList(ref List<int> List1, ref List<int> List2)
        {
            List<int> newList = new List<int>();
            foreach (int i in List1) if (List2.Contains(i)) newList.Add(i);
            return newList;
        }

        public int listCount(ref List<int> List1)
        {   // Counts the occurrences of items in the list
            int count = 0;
            foreach (int value in List1) count = count + wdCl[value].itCount;
            return count;
        }

        public int inCommon(ref List<int> List1, ref List<int> List2)
        {   // Counts the number of items in common between the two lists
            int count = 0;
            foreach (int value in List1) if (List2.Contains(value)) ++count;
            return count;
        }

        public void printList(ref List<int> myList)
        {   // Prints a list several to the line
            string s = "  ";
            if (myList.Count == 0) return;
            foreach (int item in myList)
            {
                s = s + " " + wdCl[item].itName;
                if (s.Length > 60)
                {
                    logFile.WriteLine(s);
                    s = "  ";
                }
            }
            if (s.Length > 0) logFile.WriteLine("{0}", s);
        }

        private void readParam_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(paramFile) != true)
            {
                MessageBox.Show("File Paramfile.txt cannot be found.");
                return;
            }
            string line;
            StreamReader inParams = new StreamReader(paramFile);
            line = inParams.ReadLine();
            Param1 = float.Parse(line);
            line = inParams.ReadLine();
            Param2 = int.Parse(line);
            line = inParams.ReadLine();
            Param3 = int.Parse(line);
            line = inParams.ReadLine();
            Param4 = float.Parse(line);
            line = inParams.ReadLine();
            Param5 = float.Parse(line);
            line = inParams.ReadLine();
            Param6 = float.Parse(line);
            line = inParams.ReadLine();
            Param7 = float.Parse(line);
            line = inParams.ReadLine();
            Param8 = float.Parse(line);
            readParam.Enabled = false;
            inParams.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logFile != null) logFile.Close();
        }
    }
}
