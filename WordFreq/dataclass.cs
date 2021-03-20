using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;

namespace WordFreq
{
    class dataclass
    {
        public string filename;
        public double[][] data;
        public double[][] pcdata; //PCA-transformed data
        public string[] unitlabels;
        public string[] featurelabels;
        public int[] unittotals;
        public int nfeatures = 0;
        public int nunits = 0;
        public int mastertotal = 0;
        public int[] mastercount;
        public double[] masterfreq;
        public int[] clusters;


        public static double pernorm = 1000000; // per million words

        public void save(string fn)
        {
            string output = JsonConvert.SerializeObject(this);
            using (StreamWriter sw = new StreamWriter(fn))
            {
                sw.WriteLine(output);
            }
        }

        public dataclass(string fn)
        {
            this.filename = fn;
        }

        public dataclass()
        {
            this.filename = "";
        }

        public static dataclass load()
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.Title = "JSon file with saved data";
            if (open1.ShowDialog() == DialogResult.OK)
            {
                return load(open1.FileName);
            }
            return null;
        }

        public static dataclass load(string fn)
        {
           using (StreamReader sr = new StreamReader(fn))
            {
                string line = sr.ReadLine();
                dataclass dc = JsonConvert.DeserializeObject<dataclass>(line);
                dc.filename = fn;
                return dc;
            }
            //return null;
        }

        public double dist(int u1, int u2)
        {
            double d = 0;
            for (int i = 0; i < nfeatures; i++)
                d += (data[u1][i] - data[u2][i]) * (data[u1][i] - data[u2][i]);
            return d;
        }

        public static List<string> badwords = new List<string>
        { "assembly","session","united","kofi","cuellar","thant","secretarygeneral", "anniversary","charter","president",
        "guterres","waldheim","boutros","kimoon","miroslav","espinosa","congratulate","congratulations","congratulation","congratulating","congratulates"};

        public static List<string> commonwords = new List<string>
        { "'s",
            "a",
"about",
"add",
"after",
"all",
"also",
"am",
"an",
"and",
"any",
"are",
"as",
"at",
"available",
"b",
"back",
"be",
"been",
"best",
"book",
"books",
"business",
"but",
"buy",
"by",
"c",
"can",
"cannot",
"center",
"city",
"click",
"company",
"contact",
"continue",
"copyright",
"could",
"countries",
            "d",
"data",
"date",
"day",
"days",
"de",
"do",
"does",
"e",
"each",
"email",
"f",
"find",
"first",
"for",
"from",
"full",
"games",
"general",
"get",
"go",
"good",
"great",
"group",
"had",
"has",
"have",
"he",
"health",
"help",
"her",
"here",
"high",
"his",
"home",
"hotel",
"how",
"i",
"if",
"in",
"info",
"information",
"into",
"is",
"it",
"item",
"items",
"its",
"jan",
"january",
"just",
"know",
"last",
"life",
"like",
"links",
"list",
"m",
"mail",
"make",
"management",
"many",
"map",
"may",
"me",
"message",
"more",
"most",
"music",
"my",
"n",
"name",
"need",
"new",
"news",
"next",
"no",
"not",
"now",
"number",
"of",
"on",
"one",
"online",
"only",
"or",
"order",
"organization",
"other",
"our",
"out",
"over",
"p",
"page",
"part",
"please",
"pm",
"post",
"price",
"product",
"products",
"program",
"r",
"re",
"read",
"real",
"reviews",
"s",
"said",
"school",
"search",
"see",
"service",
"services",
"set",
"sex",
"she",
"should",
"site",
"so",
"software",
"some",
"state",
"such",
"support",
"system",
"t",
"than",
"that",
"the",
"their",
"them",
"then",
"there",
"these",
"they",
"think",
"this",
"through",
"time",
"to",
"top",
"two",
"under",
"united",
"university",
"up",
"us",
"use",
"used",
"user",
"was",
"way",
"we",
"web",
"well",
"were",
"very",
"what",
"when",
"where",
"which",
"who",
"video",
"view",
"we",
            "will",
"with",
"work",
"world",
"would",
"x",
"year",
"years",
"you",
"your"

        };

        public bool wordfilter(string s)
        {
            bool allcommon = true;
            foreach (string w in s.Split())
            {
                if (badwords.Contains(w))
                    return false;
                if (!commonwords.Contains(w))
                    allcommon = false;
            }
            return !allcommon;
        }

        public string read_wordfreqdata(string fn)
        {
            return read_wordfreqdata(fn, 0);
        }

        public string read_wordfreqdata(string fn, int mincount)
        {
            int offset = 2;
            Dictionary<string, int> goodwords = new Dictionary<string, int>();
            //double pass, first to count feature labels and extract headers...
            using (StreamReader sr = new StreamReader(fn))
            {
                string unitlabelstring = sr.ReadLine();
                string[] ulwords = unitlabelstring.Split('\t');
                nunits = ulwords.Length - offset;
                unitlabels = new string[nunits];
                Array.Copy(ulwords, offset, unitlabels, 0, nunits);
                string totalstring = sr.ReadLine();
                unittotals = new int[nunits];
                string[] totals = totalstring.Split('\t');
                mastertotal = Convert.ToInt32(totals[1]);
                for (int i = 0; i < nunits; i++)
                    unittotals[i] = Convert.ToInt32(totals[i + offset]);
                while (!sr.EndOfStream)
                {
                    //sr.ReadLine();
                    string[] ww = sr.ReadLine().Split('\t');
                    goodwords.Add(ww[0], Convert.ToInt32(ww[1]));
                    if (goodwords[ww[0]] < mincount)
                        goodwords[ww[0]] = -1;
                    else if (!wordfilter(ww[0]))
                        goodwords[ww[0]] = -1;
                    else
                        nfeatures++;
                }
                featurelabels = new string[nfeatures];
                mastercount = new int[nfeatures];
                masterfreq = new double[nfeatures];
                clusters = new int[nunits];
                data = new double[nunits][];
                for (int i = 0; i < nunits; i++)
                {
                    data[i] = new double[nfeatures];
                    clusters[i] = 0;
                }
            }
            //double pass, second to actually read main data
            using (StreamReader sr = new StreamReader(fn))
            {
                sr.ReadLine();//throw away headers on this pass
                sr.ReadLine();
                int jf = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split('\t');
                    if (words.Length < nunits)
                        continue;
                    if (goodwords[words[0]] < 0)
                        continue;
                    featurelabels[jf] = words[0];
                    mastercount[jf] = goodwords[words[0]];
                    masterfreq[jf] = (pernorm * mastercount[jf]) / mastertotal;
                    for (int i = 0; i < nunits; i++)
                        data[i][jf] = (pernorm * (String.IsNullOrEmpty(words[i + offset]) ?0:Convert.ToDouble(words[i + offset])) / unittotals[i] - masterfreq[jf]);///masterfreq[jf];
                    jf++;
                }
                //nfeatures = jf;
            }

            return "Wordfreqdata: " + nunits + " units; " + nfeatures + " features.";
        }

        public string within_between_var(int ifeature, int[] labels)
        {
            StringBuilder sb = new StringBuilder(featurelabels[ifeature]);

            int nclusters = labels.Max() + 1;
            double[] within = new double[nclusters];
            int[] nwithin = new int[nclusters];
            int[] clustersize = new int[nclusters];
            double[] freqwithin = new double[nclusters];
            for (int ii = 0; ii < nclusters; ii++)
            {
                within[ii] = 0;
                nwithin[ii] = 0;
                clustersize[ii] = 0;
                freqwithin[ii] = 0;
            }
            double between = 0;
            int nbetween = 0;
            for (int i = 0; i < nunits; i++)
            {
                freqwithin[labels[i]] += data[i][ifeature];
                clustersize[labels[i]]++;
            }
            for (int ii = 0; ii < nclusters; ii++)
            {
                freqwithin[ii] = freqwithin[ii]/clustersize[ii];
            }

            for (int i = 0; i < nunits - 1; i++)
            {
                for (int j = i + 1; j < nunits; j++)
                {
                    if (labels[i] == labels[j])
                    {
                        within[labels[i]] += (data[i][ifeature] - data[j][ifeature]) * (data[i][ifeature] - data[j][ifeature]);
                        nwithin[labels[i]]++;
                    }
                    else
                    {
                        between += (data[i][ifeature] - data[j][ifeature]) * (data[i][ifeature] - data[j][ifeature]);
                        nbetween++;
                    }
                }
            }
            sb.Append("\t" + between / nbetween);
            for (int ii = 0; ii < nclusters; ii++)
            {
                if (nwithin[ii] > 0)
                    sb.Append("\t" + within[ii] / nwithin[ii]);
                else
                    sb.Append("\t");
            }

            sb.Append("\t" + masterfreq[ifeature]);

            for (int ii = 0; ii < nclusters; ii++)
            {
                sb.Append("\t" + (freqwithin[ii] + masterfreq[ifeature]));
            }

            int ncommon = 0;
            int icommon = -1;
            for (int ii = 0; ii < nclusters; ii++)
            {
                string eval = evaluate_cluster(within[ii] / nwithin[ii], freqwithin[ii] + masterfreq[ifeature], between / nbetween, masterfreq[ifeature]);
                if (eval == "Common")
                {
                    ncommon++;
                    icommon = ii;
                }
                sb.Append("\t" + eval);
            }
            sb.Append("\t" + ncommon);
            if (ncommon == 1)
                sb.Append("\t"+ icommon+"\t"+ (freqwithin[icommon] + masterfreq[ifeature]) / masterfreq[ifeature]);
            else
                sb.Append("\t0\t0");



            return sb.ToString();
        }

        public string evaluate_cluster(double var, double freq, double betweenvar, double masterfreq)
        {
            double factor = 3;
            if (freq < 1e-10)
                return "Missing";
            if (freq > factor * masterfreq)
                return "Common";
            else if (var < betweenvar / factor)
            {
                if (freq < masterfreq / factor)
                    return "Rare";
            }
            return "";
        }
    }
}
