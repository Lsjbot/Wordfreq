using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Accord;
using Accord.Math;
using Accord.Statistics;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Statistics.Visualizations;
using Accord.Statistics.Distributions.DensityKernels;
using Accord.MachineLearning;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace WordFreq
{
    public partial class Form1 : Form
    {

        Wordfreq db;
        static string connectionstring = "Data Source=KOMPLETT2015;Initial Catalog=\"wordfreq\";Integrated Security=True";
        SortedDictionary<string, int> mycorpus = new SortedDictionary<string, int>();
        SortedDictionary<string, int> mycapslock = new SortedDictionary<string, int>();
        List<int> hashlist = new List<int>();
        Dictionary<int, string> unhashdict = new Dictionary<int, string>();
        List<string> fullist = new List<string>();
        Dictionary<string, int> fuldict = new Dictionary<string, int>();
        Dictionary<string, int> fulstringdict = new Dictionary<string, int>();
        Dictionary<string, int> fulfreq = new Dictionary<string, int>();
        List<mailclass> fulmail = new List<mailclass>();

        Dictionary<string, int> countrypcadict = new Dictionary<string, int>()
        {
            {"TUV",0},
            {"TON",1},
            {"SLB",2},
            {"NRU",3},
            {"FSM",4},
            {"WSM",5},
            {"MHL",6},
            {"KIR",7},
            {"LCA",8},
            {"KNA",9},
            {"BRB",10},
            {"CHE",11},
            {"SWE",12},
            {"NOR",13},
            {"PNG",14},
            {"FJI",15},
            {"NZL",16},
            {"TTO",17},
            {"PLW",18},
            {"BHS",19},
            {"ATG",20},
            {"LIE",21},
            {"JAM",22},
            {"VUT",23},
            {"DMA",24},
            {"DNK",25},
            {"ISL",26},
            {"VCT",27},
            {"CAN",28},
            {"AND",29},
            {"GRD",30},
            {"GBR",31},
            {"IRL",32},
            {"BLZ",33},
            {"GUY",34},
            {"USA",35},
            {"SYC",36},
            {"VAT",37},
            {"FIN",38},
            {"MYS",39},
            {"MDV",40},
            {"AUS",41},
            {"SGP",42},
            {"EST",43},
            {"DEU",44},
            {"LVA",45},
            {"FRA",46},
            {"MCO",47},
            {"ITA",48},
            {"ISR",49},
            {"PSE",50},
            {"BTN",51},
            {"BGD",52},
            {"IND",53},
            {"LTU",54},
            {"MEX",55},
            {"ESP",56},
            {"URY",57},
            {"ECU",58},
            {"LUX",59},
            {"BRA",60},
            {"MLT",61},
            {"PAK",62},
            {"MUS",63},
            {"JOR",64},
            {"NLD",65},
            {"BEL",66},
            {"LBN",67},
            {"IDN",68},
            {"COL",69},
            {"AUT",70},
            {"BRN",71},
            {"SVN",72},
            {"LKA",73},
            {"PER",74},
            {"ERI",75},
            {"VEN",76},
            {"EGY",77},
            {"TUR",78},
            {"PHL",79},
            {"THA",80},
            {"EU",81},
            {"CRI",82},
            {"GHA",83},
            {"BOL",84},
            {"QAT",85},
            {"NPL",86},
            {"ARM",87},
            {"CHL",88},
            {"DZA",89},
            {"KWT",90},
            {"SAU",91},
            {"KEN",92},
            {"CYP",93},
            {"NGA",94},
            {"CMR",95},
            {"MNE",96},
            {"IRQ",97},
            {"CPV",98},
            {"BIH",99},
            {"MDG",100},
            {"GEO",101},
            {"PRT",102},
            {"ZAF",103},
            {"CUB",104},
            {"TUN",105},
            {"CIV",106},
            {"GRC",107},
            {"RUS",108},
            {"SLV",109},
            {"BHR",110},
            {"JPN",111},
            {"LSO",112},
            {"GMB",113},
            {"DJI",114},
            {"BFA",115},
            {"MMR",116},
            {"ARG",117},
            {"SEN",118},
            {"SLE",119},
            {"SOM",120},
            {"ETH",121},
            {"NIC",122},
            {"ARE",123},
            {"HND",124},
            {"POL",125},
            {"SUR",126},
            {"YUG",127},
            {"MAR",128},
            {"TLS",129},
            {"LBY",130},
            {"ROU",131},
            {"NER",132},
            {"TZA",133},
            {"SYR",134},
            {"LBR",135},
            {"SWZ",136},
            {"OMN",137},
            {"GTM",138},
            {"MLI",139},
            {"TGO",140},
            {"PAN",141},
            {"HRV",142},
            {"BWA",143},
            {"ZMB",144},
            {"GAB",145},
            {"CHN",146},
            {"ZWE",147},
            {"NAM",148},
            {"COM",149},
            {"TKM",150},
            {"SRB",151},
            {"UGA",152},
            {"IRN",153},
            {"MWI",154},
            {"UKR",155},
            {"SVK",156},
            {"BEN",157},
            {"KAZ",158},
            {"BLR",159},
            {"PRY",160},
            {"SMR",161},
            {"HUN",162},
            {"HTI",163},
            {"SDN",164},
            {"RWA",165},
            {"GNB",166},
            {"BDI",167},
            {"TJK",168},
            {"CSK",169},
            {"YEM",170},
            {"MRT",171},
            {"ALB",172},
            {"STP",173},
            {"YDYE",174},
            {"GIN",175},
            {"VNM",176},
            {"UZB",177},
            {"AZE",178},
            {"DOM",179},
            {"BGR",180},
            {"GNQ",181},
            {"MOZ",182},
            {"TCD",183},
            {"COG",184},
            {"COD",185},
            {"KHM",186},
            {"KGZ",187},
            {"AFG",188},
            {"CZE",189},
            {"AGO",190},
            {"MNG",191},
            {"MKD",192},
            {"SSD",193},
            {"LAO",194},
            {"MDA",195},
            {"CAF",196},
            {"KOR",197},
            {"DDR",198},
            {"PRK",199}

        };

        class mcbfclass
        {
            public string word = "";
            public int number = 0;
            public double freq = 0;
            public double freqdb = 0;
            public double sigma = 0;
            public double diffdb = 0;
        }

        public Form1()
        {
            InitializeComponent();
            db = new Wordfreq(connectionstring);

            fuldict.Add("apa", 1);
            fuldict.Add("avgrundsdjup", 1);
            fuldict.Add("avgrundsdjupa", 1);
            fuldict.Add("begåvningshandikappade", 2);
            fuldict.Add("begåvningsreserv", 2);
            fuldict.Add("begåvningsreserven", 2);
            fuldict.Add("bluffa", 1);
            fuldict.Add("bluffakturera", 1);
            fuldict.Add("blåsning", 1);
            fuldict.Add("blåst", 1);
            fuldict.Add("brott", 1);
            fuldict.Add("brottet", 1);
            fuldict.Add("död", 1);
            fuldict.Add("dum", 1);
            fuldict.Add("dumma", 1);
            fuldict.Add("falsk", 1);
            fuldict.Add("falska", 1);
            fuldict.Add("falskhet", 1);
            fuldict.Add("falskt", 1);
            fuldict.Add("fan", 1);
            fuldict.Add("förbannat", 1);
            fuldict.Add("förfalska", 1);
            fuldict.Add("förfalskad", 1);
            fuldict.Add("förfalskade", 1);
            fuldict.Add("förfalskar", 1);
            fuldict.Add("förfalskas", 1);
            fuldict.Add("förfalskat", 1);
            fuldict.Add("förfalskats", 1);
            fuldict.Add("förfalskning", 1);
            fuldict.Add("förfalskningar", 1);
            fuldict.Add("förståndshandikappade", 3);
            fuldict.Add("genomrutten", 1);
            fuldict.Add("helvetete", 1);
            fuldict.Add("helvete", 1);
            fuldict.Add("hora", 3);
            fuldict.Add("hora???då", 3);
            fuldict.Add("horor=", 3);
            fuldict.Add("horan", 3);
            fuldict.Add("registratorshora", 3);
            fuldict.Add("registratorshoran", 3);
            fuldict.Add("horor", 3);
            fuldict.Add("hororna", 3);
            fuldict.Add("hjärnor", 1);
            fuldict.Add("hjärndöd", 2);
            fuldict.Add("hjärndött", 2);
            fuldict.Add("hjärndöda", 2);
            fuldict.Add("idiot", 3);
            fuldict.Add("idiotiska", 1);
            fuldict.Add("jäv", 1);
            fuldict.Add("jävet", 1);
            fuldict.Add("jävig", 1);
            fuldict.Add("jäviga", 1);
            fuldict.Add("jävla", 1);
            fuldict.Add("jhävla", 1);
            fuldict.Add("jväla", 1);
            fuldict.Add("jävligt", 1);
            fuldict.Add("jäääävla", 1);
            fuldict.Add("korrupt", 2);
            fuldict.Add("korrupta", 2);
            fuldict.Add("korrutpra", 2);
            fuldict.Add("korrupota", 2);
            fuldict.Add("korurpta", 2);
            fuldict.Add("lekstugenivå", 1);
            fuldict.Add("ljgua", 1);
            fuldict.Add("ljug", 1);
            fuldict.Add("ljuga", 1);
            fuldict.Add("ljugande", 1);
            fuldict.Add("ljugas", 1);
            fuldict.Add("ljuger", 1);
            fuldict.Add("beljuger", 1);
            fuldict.Add("ljugit", 1);
            fuldict.Add("ljuuuger", 1);
            fuldict.Add("ljuuuuger", 1);
            fuldict.Add("ljög", 1);
            fuldict.Add("ljööööög", 1);
            fuldict.Add("lögen", 1);
            fuldict.Add("lögn", 1);
            fuldict.Add("lögnen", 1);
            fuldict.Add("lögn?´", 1);
            fuldict.Add("lögn`", 1);
            fuldict.Add("lögnae", 1);
            fuldict.Add("lögnare", 2);
            fuldict.Add("lögnaren", 2);
            fuldict.Add("lögnarhora", 3);
            fuldict.Add("lögnarhoror", 3);
            fuldict.Add("lögnarhögskolan", 1);
            fuldict.Add("lögnar-rodin", 2);
            //fuldict.Add("lögnen", 1);
            fuldict.Add("lögner", 1);
            fuldict.Add("lögnerna", 1);
            fuldict.Add("lögnhals", 2);
            fuldict.Add("löööööögn", 1);
            fuldict.Add("miffo", 2);
            fuldict.Add("mlögn", 1);
            fuldict.Add("mobba", 1);
            fuldict.Add("mobbar", 1);
            fuldict.Add("mobbas", 1);
            fuldict.Add("osann", 1);
            fuldict.Add("osanning", 1);
            fuldict.Add("osant", 1);
            fuldict.Add("pårökt", 2);
            fuldict.Add("pårökta", 2);
            fuldict.Add("repressalielögner", 2);
            fuldict.Add("sanslöst", 1);
            fuldict.Add("skit", 1);
            fuldict.Add("skita", 1);
            fuldict.Add("skiter", 1);
            fuldict.Add("skitit", 1);
            fuldict.Add("skitsnack", 1);
            fuldict.Add("supporthororna", 3);
            fuldict.Add("taskigt", 1);
            fuldict.Add("totalljuger", 1);
            fuldict.Add("totaljuger", 1);
            fuldict.Add("totalljugit", 1);
            fuldict.Add("vidriga", 1);
            fuldict.Add("verklighetsförfalskning", 1);
            fuldict.Add("värdelösa", 1);
            fuldict.Add("support@du.se-hororna", 3);
            //fullist.Add("apa");
            //fullist.Add("avgrundsdjup");
            //fullist.Add("avgrundsdjupa");
            //fullist.Add("begåvningshandikappade");
            //fullist.Add("begåvningsreserv");
            //fullist.Add("begåvningsreserven");
            //fullist.Add("bluffa");
            //fullist.Add("bluffakturera");
            //fullist.Add("blåsning");
            //fullist.Add("blåst");
            //fullist.Add("brott");
            //fullist.Add("brottet");
            //fullist.Add("falsk");
            //fullist.Add("falska");
            //fullist.Add("falskhet");
            //fullist.Add("falskt");
            //fullist.Add("fan");
            //fullist.Add("förfalskad");
            //fullist.Add("förfalskade");
            //fullist.Add("förfalskar");
            //fullist.Add("förfalskas");
            //fullist.Add("förfalskat");
            //fullist.Add("förfalskats");
            //fullist.Add("förfalskning");
            //fullist.Add("förståndshandikappade");
            //fullist.Add("helvetete");
            //fullist.Add("hora");
            //fullist.Add("horor");
            //fullist.Add("jävla");
            //fullist.Add("jäääävla");
            //fullist.Add("korrupt");
            //fullist.Add("korrupta");
            //fullist.Add("korrutpra");
            //fullist.Add("ljgua");
            //fullist.Add("ljug");
            //fullist.Add("ljuga");
            //fullist.Add("ljugande");
            //fullist.Add("ljugas");
            //fullist.Add("ljuger");
            //fullist.Add("ljugit");
            //fullist.Add("ljuuuger");
            //fullist.Add("ljuuuuger");
            //fullist.Add("ljög");
            //fullist.Add("ljööööög");
            //fullist.Add("lögen");
            //fullist.Add("lögn");
            //fullist.Add("lögn?´");
            //fullist.Add("lögn`");
            //fullist.Add("lögnae");
            //fullist.Add("lögnare");
            //fullist.Add("lögnaren");
            //fullist.Add("lögnarhora");
            //fullist.Add("lögnarhoror");
            //fullist.Add("lögnarhögskolan");
            //fullist.Add("lögnar-rodin");
            //fullist.Add("lögnen");
            //fullist.Add("lögner");
            //fullist.Add("lögnerna");
            //fullist.Add("lögnhals");
            //fullist.Add("löööööögn");
            //fullist.Add("mlögn");
            //fullist.Add("mobba");
            //fullist.Add("mobbar");
            //fullist.Add("mobbas");
            //fullist.Add("osann");
            //fullist.Add("osanning");
            //fullist.Add("osant");
            //fullist.Add("pårökt");
            //fullist.Add("pårökta");
            //fullist.Add("repressalielögner");
            //fullist.Add("skita");
            //fullist.Add("skiter");
            //fullist.Add("skitit");
            //fullist.Add("skitsnack");
            //fullist.Add("totalljuger");
            //fullist.Add("totalljugit");
            //fullist.Add("verklighetsförfalskning");

        }


        public void memo(string s)
        {
            richTextBox1.AppendText(s + "\n");
            richTextBox1.ScrollToCaret();

        }

        public void makebold(string s, int startpos, RichTextBox rtb)
        {
            int pos1 = richTextBox1.Text.IndexOf(s + " ", startpos);
            if (pos1 > 0)
            {
                rtb.Select(pos1, s.Length);
                rtb.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
                makebold(s, pos1 + 1, rtb);
            }
            else if (richTextBox1.Text.IndexOf(s, startpos) > 0)
            {
                pos1 = richTextBox1.Text.IndexOf(s, startpos);
                rtb.Select(pos1, s.Length);
                rtb.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
                makebold(s, pos1 + 1, rtb);
            }
            else if (s != s.ToUpper())
            {
                makebold(s.ToUpper(), startpos, rtb);
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int tryconvert(string word)
        {
            int i = -1;

            try
            {
                i = Convert.ToInt32(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Int32 type: " + word);
            }
            catch (FormatException)
            {
                //if ( !String.IsNullOrEmpty(word))
                //    Console.WriteLine("i Not in a recognizable format: " + word);
            }

            return i;

        }

        public static double tryconvertdouble(string word)
        {
            double i = -1;

            try
            {
                i = Convert.ToDouble(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Double type: " + word);
            }
            catch (FormatException)
            {
                try
                {
                    i = Convert.ToDouble(word.Replace(".", ","));
                }
                catch (FormatException)
                {
                    Console.WriteLine("i Not in a recognizable double format: " + word.Replace(".", ","));
                }
                //Console.WriteLine("i Not in a recognizable double format: " + word);
            }

            return i;

        }


        public static decimal? tryconvertdecimal(string word)
        {
            //decimal i = -1;

            double x = tryconvertdouble(word);
            if ((x >= 0) || (word.Contains('-')))
                return (decimal)x;
            else
                return null;

            //try
            //{
            //    i = Convert.ToDecimal(word.Replace(".", ","));
            //}
            //catch (OverflowException)
            //{
            //    Console.WriteLine("i Outside the range of the Decimal type: " + word);
            //    return null;
            //}
            //catch (FormatException)
            //{
            //    try
            //    {
            //        i = Convert.ToDecimal(word);
            //    }
            //    catch (FormatException)
            //    {
            //        Console.WriteLine("i Not in a recognizable decimal format: " + word.Replace(".", ","));
            //        return null;
            //    }
            //    //Console.WriteLine("i Not in a recognizable double format: " + word);
            //}

            //return i;

        }

        static string punctuation = ".,!?;:()–-\"'*\\/_…”[]";
        static char[] punctuationchars = punctuation.ToCharArray();

        private string cleanword(string w)
        {
            string ww = w.Trim(punctuationchars).ToLower();
            Regex rex = new Regex(@"\d");
            if (rex.IsMatch(ww))
                return null;

            return ww;
        }

        private void ShowCorpus(SortedDictionary<string, int> mycorpus, double nword)
        {
            int nww = 0;
            List<mcbfclass> mcbf = new List<mcbfclass>();
            foreach (string ww in mycorpus.Keys)
            {
                mcbfclass mc = new mcbfclass();
                mc.word = ww;
                mc.number = mycorpus[ww];
                mc.freq = 1000000 * (mycorpus[ww] / nword);
                var qq = (from c in db.Word where c.Token == mc.word select c);
                double ff = 0;
                double nn = 0;
                foreach (Word c in qq)
                {
                    ff += (double)c.Frequency;
                    nn += (double)c.Number;
                }
                mc.freqdb = ff;
                double minsig = 0;
                double sig = minsig;
                if (nn > 0)
                {
                    sig = ff / Math.Sqrt(nn);
                    if (sig < minsig)
                        sig = minsig;
                }
                mc.sigma = (mc.freq - mc.freqdb) / sig;

                mc.diffdb = mc.freq - mc.freqdb;
                if (mc.freqdb > 1)
                    mc.diffdb = mc.diffdb / mc.freqdb;

                mcbf.Add(mc);
                nww++;
                if (nww % 100 == 0)
                    memo("nww = " + nww);
            }

            var q = mcbf.OrderByDescending(c => c.diffdb);
            foreach (mcbfclass c in q)
            {
                string isful = fuldict.ContainsKey(c.word) ? "***" : "";
                //if ( c.number > 10)
                memo(c.word + "\t" + c.number.ToString() + "\t" + c.freq.ToString("N2") + "\t" + c.freqdb.ToString("N2") + "\t" + c.sigma.ToString("N2") + "\t" + c.diffdb.ToString("N2") + "\t" + isful);

            }

        }

        private hashcorpusclass readcorpusfile(string fn)
        {
            memo("Reading " + fn);
            string fnleaf = fn.Substring(fn.LastIndexOf('\\') + 1);
            hashcorpusclass hc = new hashcorpusclass();
            hc.countrycode = fnleaf.Substring(0, fnleaf.IndexOf('_'));
            hc.year = Convert.ToInt32(fnleaf.Substring(fnleaf.LastIndexOf('_') + 1, 4));

            char[] splitchars = " .,?!:;()[]“”\"".ToCharArray();
            string genitive = "'s";
            Regex rnumber = new Regex(@"\d+");

            using (StreamReader sr = new StreamReader(fn))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Contains('\t'))
                        line = line.Split('\t')[1];
                    foreach (string w in line.Split(splitchars))
                        if (!string.IsNullOrEmpty(w))
                        {
                            if (rnumber.Matches(w).Count > 0) //skip numbers
                                continue;
                            string ww = w.Replace("’", "'");
                            if (ww.EndsWith(genitive))
                            {
                                hc.addword(genitive);
                                hc.addword(ww.Replace(genitive, ""));
                            }
                            else
                                hc.addword(ww);
                        }
                }
            }

            hc.yearcountrymerge();

            return hc;
        }

        private List<hashcorpusclass> docorpusdirectory(string dirname)
        {
            memo("Reading " + dirname);
            List<hashcorpusclass> hl = new List<hashcorpusclass>();
            string[] filelist = Directory.GetFiles(dirname);
            foreach (string fn in filelist)
                if (fn.Contains(".txt") && !fn.Contains("\\.DS_"))
                    hl.Add(readcorpusfile(fn));
            string[] subdirlist = Directory.GetDirectories(dirname);
            foreach (string dn in subdirlist)
                foreach (hashcorpusclass hc in docorpusdirectory(dn))
                    hl.Add(hc);
            return hl;
        }

        private void savewordfreq(List<hashcorpusclass> clist, string fn, List<string> selwords)
        {
            using (StreamWriter sw = new StreamWriter(unused_filename(fn)))
            {
                StringBuilder sbhead = new StringBuilder("Wordfreq\tMASTER");
                foreach (hashcorpusclass hc in clist)
                {
                    sbhead.Append("\t" + hc.countrycode + "-" + hc.year);
                }
                sw.WriteLine(sbhead.ToString());
                StringBuilder sbcount = new StringBuilder("\t" + hashcorpusclass.mastercorpus.wordfreqdict.Sum(x => x.Value));
                foreach (hashcorpusclass hc in clist)
                {
                    sbcount.Append("\t" + hc.wordfreqdict.Sum(x => x.Value));
                }
                sw.WriteLine(sbcount.ToString());

                foreach (int nw in hashcorpusclass.hashtoworddict.Keys)
                {
                    string w = hashcorpusclass.hashtoworddict[nw];
                    if (CB_selwords.Checked)
                    {
                        if (!selwords.Contains(w))
                            continue;
                    }
                    StringBuilder sb = new StringBuilder(w + "\t" + hashcorpusclass.mastercorpus.wordfreqdict[nw]);
                    foreach (hashcorpusclass hc in clist)
                    {
                        if (hc.wordfreqdict.ContainsKey(nw))
                            sb.Append("\t" + hc.wordfreqdict[nw]);
                        else
                            sb.Append("\t0");
                    }
                    sw.WriteLine(sb.ToString());
                }
            }

        }

        private void savengramfreq(List<hashcorpusclass> clist, string fn)
        {
            using (StreamWriter sw = new StreamWriter(unused_filename(fn)))
            {
                StringBuilder sbhead = new StringBuilder("n-gram "+TB_ngram.Text+"\tMASTER");
                foreach (hashcorpusclass hc in clist)
                {
                    sbhead.Append("\t" + hc.countrycode + "-" + hc.year);
                }
                sw.WriteLine(sbhead.ToString());
                StringBuilder sbcount = new StringBuilder("\t" + hashcorpusclass.mastercorpus.wordfreqdict.Sum(x => x.Value));
                foreach (hashcorpusclass hc in clist)
                {
                    sbcount.Append("\t" + hc.wordfreqdict.Sum(x => x.Value));
                }
                sw.WriteLine(sbcount.ToString());

                memo(hashcorpusclass.mastercorpus.ngramcountdict.Count + " ngrams");
                int nn = 0;
                foreach (string s in hashcorpusclass.mastercorpus.ngramcountdict.Keys)
                {
                    StringBuilder sb = new StringBuilder(hashcorpusclass.decode_ngram(s));
                    nn++;
                    if (nn % 100 == 0)
                        memo(nn + ": " + sb.ToString());
                    sb.Append("\t" + hashcorpusclass.mastercorpus.ngramcountdict[s]);
                    foreach (hashcorpusclass hc in clist)
                    {
                        if (hc.ngramcountdict.ContainsKey(s))
                            sb.Append("\t" + hc.ngramcountdict[s]);
                        else
                            sb.Append("\t0");
                    }
                    sw.WriteLine(sb.ToString());
                }

            }

        }

        private void ReadCorpusButton_Click(object sender, EventArgs e)
        {
            //Dictionary<string, int> wordfreqdict = new Dictionary<string, int>();

            hashcorpusclass.corpuslist.Clear();
            hashcorpusclass.hashtoworddict.Clear();
            hashcorpusclass.wordtohashdict.Clear();


            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string dirname = folderBrowserDialog1.SelectedPath;
                hashcorpusclass.corpuslist = docorpusdirectory(dirname);
                memo("#corpora = " + hashcorpusclass.corpuslist.Count);
                memo(hashcorpusclass.corpuslist[0].print_wordfreq());

                //hashcorpusclass mastercorpus = new hashcorpusclass();
                foreach (hashcorpusclass hc in hashcorpusclass.yearcorpus.Values)
                    hashcorpusclass.mastercorpus.merge(hc);
                //memo(hashcorpusclass.mastercorpus.print_wordfreq());

                memo("Fill z-scores for master");
                hashcorpusclass.mastercorpus.fill_zscores(10, db);
                string output = hashcorpusclass.mastercorpus.print_z_var();
                memo(output);
                string fnout = @"D:\Ling\UNcorpus-wordfreq-out-0.txt";
                using (StreamWriter sw = new StreamWriter(unused_filename(fnout)))
                {
                    sw.WriteLine(output);
                }
                //memo(hashcorpusclass.corpuslist[0].print_ngrams(10));

                if (CB_wordfreq.Checked)
                {
                    List<string> selwords = new List<string>();
                    if (CB_selwords.Checked)
                    {
                        openFileDialog1.Title = "File with Selectwords:";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                            {
                                sr.ReadLine();
                                while (!sr.EndOfStream)
                                {
                                    selwords.Add(sr.ReadLine());
                                }
                            }
                        }
                    }
                    string fnout2 = @"D:\Ling\UNcorpus-wordfreq-subs-year-0.txt";
                    savewordfreq(hashcorpusclass.yearcorpus.Values.ToList(), fnout2,selwords);
                    string fnout3 = @"D:\Ling\UNcorpus-wordfreq-subs-country-0.txt";
                    savewordfreq(hashcorpusclass.countrycorpus.Values.ToList(), fnout3, selwords);
                    string fnout4 = @"D:\Ling\UNcorpus-wordfreq-subs-all-0.txt";
                    savewordfreq(hashcorpusclass.corpuslist, fnout4, selwords);

                }

            }

            if (CB_ngrams.Checked)
            {
                hashcorpusclass.mastercorpus.find_ngrams(TB_ngram.Value, false);
                hashcorpusclass.mastercorpus.ngram_cutoff(20);
                memo(hashcorpusclass.mastercorpus.print_ngrams(20));

                foreach (hashcorpusclass hc in hashcorpusclass.corpuslist)
                {
                    memo("Find ngrams for " + hc.countrycode + " " + hc.year);
                    hc.find_ngrams(TB_ngram.Value, true);
                }

                foreach (hashcorpusclass hc in hashcorpusclass.yearcorpus.Values)
                {
                    memo("Find ngrams for " + hc.countrycode + " " + hc.year);
                    hc.find_ngrams(TB_ngram.Value, true);
                }

                foreach (hashcorpusclass hc in hashcorpusclass.countrycorpus.Values)
                {
                    memo("Find ngrams for " + hc.countrycode + " " + hc.year);
                    hc.find_ngrams(TB_ngram.Value, true);
                }

                //memo(hashcorpusclass.corpuslist[0].print_ngrams(10));

                string fnout = @"D:\Ling\UNcorpus-ngram-out-" + TB_ngram.Value + "-0.txt";
                savengramfreq(hashcorpusclass.corpuslist, fnout);

                string fnouty = @"D:\Ling\UNcorpus-ngram-year-out-" + TB_ngram.Value + "-0.txt";
                savengramfreq(hashcorpusclass.yearcorpus.Values.ToList(), fnouty);

                string fnoutc = @"D:\Ling\UNcorpus-ngram-country-out-" + TB_ngram.Value + "-0.txt";
                savengramfreq(hashcorpusclass.countrycorpus.Values.ToList(), fnoutc);

            }

        }
    

        private string unused_filename(string fn)
        {
            string fnnew = fn;
            int n = 1;
            while (File.Exists(fnnew))
            {
                fnnew = fn.Replace(".", n + ".");
                n++;
            }
            return fnnew;
        }

        private void DatabaseButton_Click(object sender, EventArgs e)
        {
            //English word frequency table from https://www.kaggle.com/rtatman/english-word-frequency 

            string fn = @"d:\Ling\unigram_freq.csv";

            long totalwordcount = 588124220187;
            double wordcount = totalwordcount;

            long nwtotal = 0;
            int n = 0;
            int maxlength = 0;
            using (StreamReader sr = new StreamReader(fn))
            {
                sr.ReadLine(); //throw away header
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split(',');
                    string token = words[0];
                    if (token.Length > maxlength)
                        maxlength = token.Length;
                    long tokencount = Convert.ToInt64(words[1]);
                    double freq = tokencount / wordcount;
                    //memo(token + " " + freq);
                    nwtotal += tokencount;

                    EnglishWords ew = new EnglishWords();
                    ew.Token = token;
                    ew.Number = tokencount;
                    ew.Freq = freq;
                    db.EnglishWords.InsertOnSubmit(ew);
                    db.SubmitChanges();
                    n++;
                    if (n % 10000 == 0)
                        memo(n.ToString());

                    
                    //if (n > 100)
                    //    break;
                }
                memo(n + " tokens");
                memo(nwtotal + " total count");
                memo("Maxlength = " + maxlength);

            }

            ////Swedish word frequency table:
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    memo("Clearing database");
            //    db.Word.DeleteAllOnSubmit(from c in db.Word select c);
            //    db.SubmitChanges();
            //    memo("Clearing done");

            //    int n = 0;
            //    int idb = 1;
            //    if ((from c in db.Word select c).Count() > 0)
            //        idb = (from c in db.Word select c.Id).Max() + 1;

            //    string punctuation = ".,!?;:()–-\"'";
            //    char[] punctuationchars = punctuation.ToCharArray();
            //    try
            //    {
            //        using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            //        {

            //                while (!sr.EndOfStream)
            //                {
            //                    String line = sr.ReadLine();
            //                    if (string.IsNullOrEmpty(line))
            //                        continue;
            //                    memo(line);

            //                    string[] words = line.Split('\t');
            //                    if (words.Length < 6)
            //                        continue;

            //                    Word wdb = new Word();
            //                    wdb.Id = idb;
            //                    idb++;
            //                    if (words[0].Length > 50)
            //                        words[0] = words[0].Substring(0, 50);
            //                    wdb.Token = words[0].Trim();
            //                    if (words[1].Length > 50)
            //                        words[1] = words[1].Substring(0, 50);
            //                    wdb.Class = words[1].Trim();
            //                    if (words[2].Length > 150)
            //                        words[2] = words[2].Substring(0, 150);
            //                    wdb.Linkstring = words[2].Trim();
            //                    wdb.Composed = (words[3].Contains("+"));
            //                    wdb.Number = tryconvert(words[4]);
            //                    wdb.Frequency = tryconvertdecimal(words[5]);
            //                    if (wdb.Frequency < 5)
            //                        break;

            //                    db.Word.InsertOnSubmit(wdb);
            //                    db.SubmitChanges();

            //                    n++;

            //                    //if (n > 20)
            //                    //    break;

            //                }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            //    }
            //}



        }

        private void button_readmail_Click(object sender, EventArgs e)
        {
            double nword = 0;
            double ncaps = 0;
            int nful = 0;
            int fulrow = 0;
            string fulstring = "";
            string prevline = "";
            mailclass currentmail = new mailclass();
            bool skiprest = false;
            bool fulfound = false;
            int fulscore = 0;

            //Joakim:
            //string newmailmarker = "RequestID:";
            //string skipmarker = "-----Ursprungligt";
            //int fulmin = 3;
            //Magdalena Wänderstam:
            string newmailmarker = "-----";
            string skipmarker = "&&&&&&&&&&&&&&&&&";
            int fulmin = 4;
            char[] trimchars = "> ".ToCharArray();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in openFileDialog1.FileNames)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(fn))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();//.Trim(trimchars);
                                if (String.IsNullOrEmpty(line))
                                    continue;
                                line = line.Trim(trimchars);

                                int i = line.GetHashCode();
                                if (!line.StartsWith(newmailmarker))
                                {
                                    if (hashlist.Contains(i))
                                        continue;
                                    else
                                        hashlist.Add(i);
                                }
                                string[] words = line.Split();
                                if (words.Length == 0)
                                    continue;
                                if (line.StartsWith(newmailmarker) || sr.EndOfStream)
                                {
                                    memo("fulscore = " + fulscore);
                                    if (fulscore >= fulmin)
                                    {
                                        fulmail.Add(currentmail);
                                    }
                                    currentmail = new mailclass();
                                    if (words.Length > 1)
                                        currentmail.reqid = tryconvert(words[1]);
                                    skiprest = false;
                                    fulfound = false;
                                    fulscore = 0;
                                }
                                else if ((words[0] == "Date:") || (words[0] == "Skickat:") || (words[0] == "Sent:"))
                                    currentmail.maildate = line;
                                else
                                    currentmail.mailbody += " " + line;

                                if (line.Contains(skipmarker))
                                    skiprest = true;
                                if (skiprest)
                                    continue;

                                foreach (string w in words)
                                {
                                    string ww = cleanword(w);
                                    if (String.IsNullOrEmpty(ww))
                                        continue;
                                    if (fuldict.ContainsKey(ww))
                                    {
                                        fulfound = true;
                                        fulrow++;
                                        fulstring += " " + ww;
                                        fulscore += fuldict[ww];
                                        if (!fulfreq.ContainsKey(ww))
                                            fulfreq.Add(ww, 1);
                                        else
                                            fulfreq[ww]++;
                                        //memo("fulscore = " + fulscore);
                                    }
                                    else if (fulrow > 0)
                                    {
                                        //if ((fulrow > 1 )|| (fuldict[fulstring.Trim()] > 1))
                                        {
                                            if (!fulstringdict.ContainsKey(fulstring))
                                                fulstringdict.Add(fulstring, 1);
                                            else
                                                fulstringdict[fulstring]++;
                                            currentmail.badstrings.Add(fulstring);
                                        }
                                        fulstring = "";
                                        fulrow = 0;
                                    }
                                    if (!mycorpus.ContainsKey(ww))
                                        mycorpus.Add(ww, 0);
                                    mycorpus[ww]++;
                                    if (w.ToUpper() == w)
                                    {
                                        if (!mycapslock.ContainsKey(ww))
                                            mycapslock.Add(ww, 0);
                                        mycapslock[ww]++;
                                        ncaps++;
                                    }
                                    nword++;
                                    //if (nword % 10000 == 0)
                                    //    memo(nword + " words");
                                }
                                //if ( fulfound)
                                //{
                                //    nful++;
                                //    if (fulscore > 1)
                                //    {
                                //        if (words.Length < 15)
                                //            memo(prevline);
                                //        memo(line);
                                //        memo("=====================");
                                //    }
                                //}
                                prevline = line;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                //ShowCorpus(mycorpus,nword);
                //memo("\n\nMYCAPSLOCK\n");
                //ShowCorpus(mycapslock,ncaps);

                foreach (string ww in fulfreq.Keys)
                {
                    memo(ww + "\t" + fulfreq[ww].ToString());
                }

                memo("========= fulstringdict =============");
                foreach (string ww in fulstringdict.Keys)
                {
                    memo(ww + "\t" + fulstringdict[ww].ToString());
                }
                //foreach (string ww in mycorpus.Keys)
                //{
                //    if ( mycorpus[ww] > 10)
                //        memo(ww + "\t" + mycorpus[ww].ToString());
                //}

                memo("========= fulmail =============");
                foreach (mailclass mm in fulmail)
                {
                    memo("----------------------------------------------");
                    memo("Reqid: " + mm.reqid);
                    memo("Date: " + mm.maildate);
                    int startpos = richTextBox1.Text.Length;
                    memo(mm.mailbody);
                    foreach (string s in mm.badstrings)
                        makebold(s, startpos, richTextBox1);
                }

            }
            memo("Done");

        }

        private void NgramButton_Click(object sender, EventArgs e)
        {
            double[] dd = new double[] { 1, 2, 3, 4 };
            memo("Variance = "+dd.Variance());
        }

        private void Wordfreqbutton_Click(object sender, EventArgs e)
        {
            List<wordclass> wordlist = new List<wordclass>();
            hbookclass yearvarhist = new hbookclass("Year variance");
            yearvarhist.SetBins(0, 2, 100);
            hbookclass countryvarhist = new hbookclass("Country variance");
            countryvarhist.SetBins(0, 2, 100);

            double minzscore = 0;
            double minyvar = 0.03;
            double cyvarratio = 0.5;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    sr.ReadLine(); //throw away header
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] words = line.Split('\t');
                        if (words.Length < 5)
                            continue;
                        wordclass wc = new wordclass();
                        wc.token = words[0];
                        wc.number = Convert.ToInt32(words[1]);
                        wc.zscore = Convert.ToDouble(words[2]);
                        wc.yearvar = Convert.ToDouble(words[3]) / wc.number;
                        wc.countryvar = Convert.ToDouble(words[4]) / wc.number;
                        wordlist.Add(wc);

                        yearvarhist.Add(wc.yearvar);
                        countryvarhist.Add(wc.countryvar);
                    }
                }
                memo(yearvarhist.GetDHist());
                memo(countryvarhist.GetDHist());

                string fnout = @"D:\Ling\selectedwords-0.txt";
                using (StreamWriter sw = new StreamWriter(unused_filename(fnout)))
                {
                    sw.WriteLine("min z-score\t" + minzscore + "\tmin year-variance\t" + minyvar + "\tmin ration country/year-var\t" + cyvarratio);
                    foreach (wordclass wc in wordlist)
                    {
                        if (wc.zscore < minzscore)
                            continue;
                        if (wc.yearvar < minyvar)
                            continue;
                        if (wc.countryvar / wc.yearvar < cyvarratio)
                            continue;
                        sw.WriteLine(wc.token);
                    }
                }
            }
        }

        private void meanshiftbutton_Click(object sender, EventArgs e)
        {
            if (CB_usewordfreq.Checked)
            {
                openFileDialog1.Title = "Frequency file";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //double iband = Math.Log(100000);
                    //For year data, bandwidth ~500
                    //For country data, bandwidth ~1000
                    for (double iband = 0; iband < 10; iband+= 0.25)
                    {
                        dataclass wordfreqdata;
                        if (openFileDialog1.FileName.EndsWith(".json"))
                        {
                            wordfreqdata = dataclass.load(openFileDialog1.FileName);
                        }
                        else
                        {
                            int mincount = 0;
                            if (openFileDialog1.FileName.Contains("ngram"))
                                mincount = 100;
                            wordfreqdata = new dataclass(openFileDialog1.FileName);
                            memo(wordfreqdata.read_wordfreqdata(openFileDialog1.FileName,mincount));
                        }

                        // Create a new Mean-Shift algorithm for 3 dimensional samples
                        MeanShift meanShift = new MeanShift()
                        {
                            // Use a uniform kernel density
                            Kernel = new UniformKernel(),
                            Maximum = 50,
                            Tolerance = 1e-2,
                            //Bandwidth = tryconvertdouble(TBmeanshift.Text)
                            Bandwidth = Math.Exp(iband)//tryconvertdouble(TBmeanshift.Text)
                        };

                        memo("Learning...");
                        this.Refresh();

                        // Learn a data partitioning using the Mean Shift algorithm
                        MeanShiftClusterCollection clustering = meanShift.Learn(wordfreqdata.data);

                        memo("Learn done");

                        // Predict group labels for each point
                        int[] labels = clustering.Decide(wordfreqdata.data);

                        Dictionary<int, int> clustersize = new Dictionary<int, int>();
                        Dictionary<int, List<string>> clusterlist = new Dictionary<int, List<string>>();

                        for (int i = 0; i < labels.Length; i++)
                        {
                            //memo(i + "\t" + wordfreqdata.unitlabels[i] + "\t" + labels[i]);
                            if (!clustersize.ContainsKey(labels[i]))
                            {
                                clustersize.Add(labels[i], 0);
                                clusterlist.Add(labels[i], new List<string>());
                            }
                            clustersize[labels[i]]++;
                            clusterlist[labels[i]].Add(wordfreqdata.unitlabels[i]);
                        }
                        int maxsize = -1;
                        foreach (int i in clustersize.Keys)
                        {
                            string s = "";
                            foreach (string ss in clusterlist[i])
                                s += " " + ss;
                            //memo(i + ": " + clustersize[i] + ";" + s);
                            if (clustersize[i] > maxsize)
                                maxsize = clustersize[i];
                        }
                        memo("# clusters: " + clustersize.Count);
                        memo("max cluster size: " + maxsize);
                        memo("Bandwith = " + meanShift.Bandwidth);
                        memo("");

                    }

                }
            }
        }

        private void Kmeansbutton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (CB_usewordfreq.Checked)
            {
                openFileDialog1.Title = "Frequency file";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    dataclass wordfreqdata;
                    if (openFileDialog1.FileName.EndsWith(".json"))
                    {
                        wordfreqdata = dataclass.load(openFileDialog1.FileName);
                    }
                    else
                    {
                        int mincount = 0;
                        if (openFileDialog1.FileName.Contains("ngram"))
                            mincount = 100;
                        wordfreqdata = new dataclass(openFileDialog1.FileName);
                        memo(wordfreqdata.read_wordfreqdata(openFileDialog1.FileName,mincount));
                    }


                    //hbookclass disthist = new hbookclass("Disthist");
                    //disthist.SetBins(0, 10000000, 50);
                    //for (int u1 = 0; u1 < wordfreqdata.nunits; u1++)
                    //    for (int u2 = 0; u2 < wordfreqdata.nunits; u2++)
                    //    {
                    //        if (rnd.Next(10000) == 1)
                    //            if (u1 != u2)
                    //                disthist.Add(wordfreqdata.dist(u1, u2));
                    //    }
                    //memo(disthist.GetDHist());
                    //return;

                    //for (int kk = 3; kk < 20; kk++)
                    int kk = Convert.ToInt32(TB_Kmeans.Text);
                    {
                        // Create a new K-Means algorithm
                        KMeans kmeans = new KMeans(k: kk);

                        memo("Clustering...");
                        // Compute and retrieve the data centroids
                        var clusters = kmeans.Learn(wordfreqdata.data);

                        memo("Clustering done");

                        // Use the centroids to parition all the data
                        int[] labels = clusters.Decide(wordfreqdata.data);

                        memo("Decide done");

                        Dictionary<int, int> clustersize = new Dictionary<int, int>();
                        Dictionary<int, List<string>> clusterlist = new Dictionary<int, List<string>>();

                        for (int i = 0; i < labels.Length; i++)
                        {
                            wordfreqdata.clusters[i] = labels[i];
                            //memo(i + "\t" + wordfreqdata.unitlabels[i] + "\t" + labels[i]);
                            if (!clustersize.ContainsKey(labels[i]))
                            {
                                clustersize.Add(labels[i], 0);
                                clusterlist.Add(labels[i], new List<string>());
                            }
                            clustersize[labels[i]]++;
                            clusterlist[labels[i]].Add(wordfreqdata.unitlabels[i]);
                        }
                        int maxsize = -1;
                        foreach (int i in clustersize.Keys)
                        {
                            string s = "";
                            foreach (string ss in clusterlist[i])
                                s += " " + ss;
                            memo(i + ": " + clustersize[i]);
                            memo(s);
                            memo("");
                            if (clustersize[i] > maxsize)
                                maxsize = clustersize[i];
                        }
                        memo("# clusters: " + clustersize.Count);
                        memo("max cluster size: " + maxsize);
                        memo("");

                        if (CB_bitmap.Checked)
                        {
                            memo("Saving bitmap");
                            Bitmap bb = makebitmap(wordfreqdata);
                            string fnbmp = unused_filename(openFileDialog1.FileName.Replace(".txt", ".bmp"));
                            bb.Save(fnbmp);
                        }

                        if (CB_savecluster.Checked)
                        {
                            string fnout = unused_filename(openFileDialog1.FileName.Replace(".txt", "kmeans"+kk+"-0.txt"));
                            memo("Writing to file " + fnout);
                            using (StreamWriter sw = new StreamWriter(fnout))
                            {
                                sw.WriteLine("Input file: " + openFileDialog1.FileName);

                                maxsize = -1;
                                foreach (int i in clustersize.Keys)
                                {
                                    string s = "";
                                    foreach (string ss in clusterlist[i])
                                        s += "\t" + ss;
                                    sw.WriteLine(i + "\t" + clustersize[i]);
                                    sw.WriteLine(s);
                                    sw.WriteLine("");
                                    if (clustersize[i] > maxsize)
                                        maxsize = clustersize[i];
                                }
                                sw.WriteLine("# clusters\t" + clustersize.Count);
                                sw.WriteLine("max cluster size\t" + maxsize);
                                sw.WriteLine("");

                                StringBuilder shead = new StringBuilder("Token\tVbetween");
                                for (int i = 0; i < clustersize.Count; i++)
                                    shead.Append("\t" + i);
                                shead.Append("\tMasterfreq");
                                for (int i = 0; i < clustersize.Count; i++)
                                    shead.Append("\t" + i);
                                for (int i = 0; i < clustersize.Count; i++)
                                    shead.Append("\t" + i);
                                //memo(shead.ToString());
                                sw.WriteLine(shead.ToString());
                                for (int ifeature = 0; ifeature < wordfreqdata.nfeatures; ifeature++)
                                {
                                    string s = wordfreqdata.within_between_var(ifeature, labels);
                                    //memo(s);
                                    sw.WriteLine(s);
                                }
                            }
                        }

                        if (CB_json.Checked)
                        {
                            memo("Saving json");
                            string fnjson = unused_filename(openFileDialog1.FileName.Replace(".txt", ".json"));
                            wordfreqdata.save(fnjson);
                        }

                        memo("Done!");
                    }
                }
            }

        }

        private Bitmap makebitmap(dataclass dc)
        {
            int pitch = 16;
            int offset = 6 * pitch;
            int ncountry = countrypcadict.Count();
            int nyear = 50;
            int startyear = 1970;

            Bitmap b = new Bitmap(offset + ncountry * pitch, offset + nyear * pitch);


            Graphics g = Graphics.FromImage(b);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;


            Dictionary<string, int> yeardict = new Dictionary<string, int>();
            yeardict.Add("1", 0);
            yeardict.Add("", 0);
            for (int i=0;i< nyear;i++)
            {
                string yearstring = (startyear + i).ToString();
                yeardict.Add(yearstring, i);
                RectangleF rectf = new RectangleF(pitch, offset+i*pitch, offset-pitch, pitch);
                g.DrawString(yearstring, new Font("Arial", pitch-3), Brushes.Black, rectf);

            }

            foreach (string s in countrypcadict.Keys)
            {
                int i = countrypcadict[s];
                RectangleF rectf = new RectangleF(offset+i*pitch, pitch, pitch, offset-pitch);
                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                g.DrawString(s, new Font("Arial", pitch-3), Brushes.Black, rectf,stringFormat);

            }

            g.Flush();

            Dictionary<int, Color> colordict = new Dictionary<int, Color>();
            colordict.Add(0, Color.Red);
            colordict.Add(1, Color.Blue);
            colordict.Add(2, Color.Yellow);
            colordict.Add(3, Color.Green);
            colordict.Add(4, Color.Orange);
            colordict.Add(5, Color.Black);
            colordict.Add(6, Color.Purple);
            colordict.Add(7, Color.LightBlue);
            colordict.Add(8, Color.Chartreuse);
            colordict.Add(9, Color.Chocolate);
            colordict.Add(10, Color.Gray);
            colordict.Add(11, Color.GreenYellow);
            colordict.Add(12, Color.HotPink);
            colordict.Add(13, Color.Brown);
            colordict.Add(14, Color.Turquoise);
            colordict.Add(15, Color.Olive);

            for (int i=0;i<dc.unitlabels.Length;i++)
            {
                string[] ss = dc.unitlabels[i].Split('-');
                string country = ss[0];
                string year = ss[1];
                int x = offset + pitch*countrypcadict[country];
                int y = offset + pitch * yeardict[year];
                SolidBrush mybrush = new SolidBrush(colordict[dc.clusters[i]]);
                g.FillRectangle(mybrush, new Rectangle(x, y, pitch, pitch));
            }
            g.Flush();

            return b;
        }

        private double[][] select_components(dataclass dc, double[][] pcdata, int pc1, int pc2)
        {
            double[][] selecteddata = new double[dc.unitlabels.Count()][];

            for (int ilang =0;ilang< dc.unitlabels.Count();ilang++)
            {
                selecteddata[ilang] = new double[2];
                selecteddata[ilang][0] = pcdata[ilang][pc1 - 1];
                selecteddata[ilang][1] = pcdata[ilang][pc2 - 1];
            }
            return selecteddata;
        }



        private void plot_pcarray2(dataclass dc, double[][] pcdata, int maxpc, int[] langclass, string title)
        {

            int nplot = maxpc * (maxpc - 1) / 2;

            int iplot = 0;
            for (int p1 = 1; p1 < maxpc; p1++)
            {
                for (int p2 = p1 + 1; p2 <= maxpc; p2++)
                {
                    //sb[iplot] = ScatterplotBox.Show("PC" + p1 + " vs. PC" + p2, select_components(dc, pcdata, p1, p2), langclass);

                    ChartForm cf = new ChartForm(chart_from_pca(select_components(dc, pcdata, p1, p2), "PC" + p1, "PC" + p2, title, langclass));
                    cf.Show();


                    iplot++;
                }
            }
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_from_pca(double[][] data, string label1, string label2, string title, int[] langclass)
        {
            Dictionary<string, int> wingdict = new Dictionary<string, int>();

            System.Windows.Forms.DataVisualization.Charting.Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart1.ChartAreas.Add(new ChartArea());

            chart1.Titles.Add("Title1");
            chart1.Titles["Title1"].Docking = Docking.Top;
            chart1.Titles["Title1"].Font = new Font("Arial", 20);
            chart1.Titles["Title1"].Text = title;


            Color[] classcolor = new Color[3] { Color.Blue, Color.Red, Color.Green };

            for (int i = 0; i < 3; i++)
            {

                Series ss = new Series(i.ToString());
                ss.ChartType = SeriesChartType.Point;
                ss.MarkerSize = 3;// TBmarkersize.Value;
                ss.MarkerColor = classcolor[i];

                //chart1.Series.Add(sslop);

                int j = 0;
                foreach (double[] dd in data)
                {
                    if (langclass[j] == i)
                        ss.Points.AddXY(dd[0], dd[1]);
                    j++;
                }

                chart1.Series.Add(ss);
            }
            chart1.ChartAreas[0].AxisX.Title = label1;
            chart1.ChartAreas[0].AxisY.Title = label2;
            double chartlimit = tryconvertdouble(TBpcachartmax.Text);

            chart1.ChartAreas[0].AxisX.Maximum = chartlimit;
            chart1.ChartAreas[0].AxisX.Minimum = -chartlimit;
            chart1.ChartAreas[0].AxisY.Maximum = chartlimit;
            chart1.ChartAreas[0].AxisY.Minimum = -chartlimit;

            chart1.ChartAreas[0].AxisX.TitleFont = new Font(FontFamily.GenericSansSerif, 20);
            chart1.ChartAreas[0].AxisY.TitleFont = new Font(FontFamily.GenericSansSerif, 20);

            return chart1;

        }



        private void PCAbutton_Click(object sender, EventArgs e)
        {
            if (CB_usewordfreq.Checked)
            {
                openFileDialog1.Title = "Wordfreq file";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    dataclass dc;
                    if (openFileDialog1.FileName.EndsWith(".json"))
                    {
                        dc = dataclass.load(openFileDialog1.FileName);
                    }
                    else
                    {
                        int mincount = 0;
                        if (openFileDialog1.FileName.Contains("ngram"))
                            mincount = 100;
                        dc = new dataclass(openFileDialog1.FileName);
                        memo(dc.read_wordfreqdata(openFileDialog1.FileName,mincount));
                    }

                    var pca = new PrincipalComponentAnalysis()
                    {
                        Method = PrincipalComponentMethod.Center,
                        Whiten = true
                    };

                    // Now we can learn the linear projection from the data
                    MultivariateLinearRegression transform = pca.Learn(dc.data);
                    //Console.WriteLine(transform.ToString());

                    // Finally, we can project all the data
                    double[][] output1 = pca.Transform(dc.data);
                    //Console.WriteLine(output1.ToString(" 0.000"));

                    for (int i=0;i<dc.nunits;i++)
                    {
                        memo(dc.unitlabels[i] + "\t" + output1[i][0]);
                    }


                    //ScatterplotBox.Show("output1", output1);
                    memo("Cumulative proportions:");
                    foreach (double d in pca.CumulativeProportions)
                        memo(d + " ");
                    //Console.WriteLine();
                    memo("Eigenvalues:");
                    foreach (double d in pca.Eigenvalues)
                        memo(d + " ");



                    // Or just its first components by setting 
                    // NumberOfOutputs to the desired components:
                    pca.NumberOfOutputs = 2;

                    // And then calling transform again:
                    double[][] output2 = pca.Transform(dc.data);

                    //int[] langclass = new int[dc.unitlabels.Count];
                    //foreach (int n in dc.langstat.Keys)
                    //{
                    //    if (dc.langstat[n] > 20)
                    //        langclass[n] = 0;
                    //    else if (dc.langstat[n] > 5)
                    //        langclass[n] = 1;
                    //    else
                    //        langclass[n] = 2;
                    //}
                    //int[] langclasscount = new int[3] { 0, 0, 0 };
                    //foreach (int n in langclass)
                    //    langclasscount[n]++;
                    //for (int i = 0; i < 3; i++)
                    //    memo(i + ":" + langclasscount[i]);

                    //plot_pcarray(dc, output1, 3, langclass);
                    plot_pcarray2(dc, output1, 3, dc.clusters, openFileDialog1.FileName);

                    //ScatterplotBox sb = ScatterplotBox.Show("PC1 vs PC2", output2, langclass);
                    //sb.SetSize(600, 600);
                    //sb.SetSymbolSize(2);
                    ////HistogramBox.Show(output2);
                    ////Console.WriteLine(output2.ToString(" 0.000"));

                    //ScatterplotBox.Show("select_components",select_components(dc,output1,1,2));

                    // We can also limit to 80% of explained variance:
                    pca.ExplainedVariance = 0.8;

                    // And then call transform again:
                    double[][] output3 = pca.Transform(dc.data);
                    //ScatterplotBox.Show("Variance 80%",output3);

                    //print_components(dc, output1, 5);

                    memo("Done");


                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            memo(DateTime.Now.ToShortTimeString());
            richTextBox1.Refresh();
        }
    }
}
