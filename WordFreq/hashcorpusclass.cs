using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Statistics;

namespace WordFreq
{
    class hashcorpusclass
    {
        public string countrycode = "";
        public int year = -1;
        public List<int> hashwords = new List<int>();
        public Dictionary<int, int> wordfreqdict = new Dictionary<int, int>();
        public Dictionary<int, double> wordzscoredict = new Dictionary<int, double>();
        public Dictionary<string, int> ngramcountdict = new Dictionary<string, int>();
        public double totalwords = 0;

        public static Dictionary<int, string> hashtoworddict = new Dictionary<int, string>();
        public static Dictionary<string,int> wordtohashdict = new Dictionary<string,int>();
        //public static Dictionary<string, ushort[]> ngramhashdict = new Dictionary<string, ushort[]>();
        public static List<hashcorpusclass> corpuslist = new List<hashcorpusclass>();
        public static hashcorpusclass mastercorpus = new hashcorpusclass();
        public static Dictionary<int, hashcorpusclass> yearcorpus = new Dictionary<int, hashcorpusclass>();
        public static Dictionary<string, hashcorpusclass> countrycorpus = new Dictionary<string, hashcorpusclass>();
        public static int minyear = 9999;
        public static int maxyear = -1;



        public static int addtoworddict(string w)
        {
            int h = hashtoworddict.Count + 1;
            if (wordtohashdict.ContainsKey(w))
                h = wordtohashdict[w];
            else
            {
                wordtohashdict.Add(w, h);
                hashtoworddict.Add(h, w);
            }
            if (!hashtoworddict.ContainsKey(h))
                hashtoworddict.Add(h, w);
            return h;
        }

        public void addword(string w)
        {
            int h = addtoworddict(w.ToLower().Replace("-","").Replace("\"",""));
            hashwords.Add(h);
            if (!wordfreqdict.ContainsKey(h))
                wordfreqdict.Add(h, 1);
            else
                wordfreqdict[h]++;
            totalwords++;
        }

        public string print_wordfreq()
        {
            return print_wordfreq(1);
        }

        public string print_wordfreq(int minfreq)
        {
            StringBuilder sb = new StringBuilder("\n\n----------------------------\n" + this.countrycode + "\t" + this.year+"\n");
            foreach (int k in wordfreqdict.Keys)
            {
                if (wordfreqdict[k] >= minfreq)
                    sb.Append(hashtoworddict[k] + "\t" + wordfreqdict[k] + "\n");
            }
            sb.Append("# different words:\t" + wordfreqdict.Count+"\n");
            sb.Append("# words total:\t" + wordfreqdict.Sum(x=>x.Value).ToString());
            return sb.ToString();
        }

        public void fill_zscores(int nmin, Wordfreq db)
        {
            foreach (int nw in wordfreqdict.Keys)
            {
                if (wordfreqdict[nw] < nmin)
                    continue;
                string w = hashtoworddict[nw];
                EnglishWords ew = (from c in db.EnglishWords
                        where c.Token == w
                        select c).FirstOrDefault();
                if (ew == null)
                    wordzscoredict.Add(nw, 999);
                else
                {
                    double varew = ew.Freq * ew.Freq / ew.Number;
                    double f = wordfreqdict[nw] / totalwords;
                    double varthis = f * f / wordfreqdict[nw];
                    double sigmadiff = Math.Sqrt(varew + varthis);
                    double zscore = (f - ew.Freq) / sigmadiff;
                    wordzscoredict.Add(nw, zscore);
                }
            }
        }

        public string print_zscores()
        {
            StringBuilder sb = new StringBuilder();
            var q = (from c in wordzscoredict select c).OrderBy(c=>c.Value);
            foreach (KeyValuePair<int,double> kp in q)
            {
                string w = hashtoworddict[kp.Key];
                sb.Append(w + "\t" + kp.Value + "\n");
            }
            return sb.ToString();
        }

        public string print_z_var()
        {
            StringBuilder sb = new StringBuilder();
            var q = (from c in wordzscoredict select c).OrderBy(c => c.Value);
            foreach (KeyValuePair<int, double> kp in q)
            {
                string w = hashtoworddict[kp.Key];
                sb.Append(w + "\t" + wordfreqdict[kp.Key] + "\t" + kp.Value + "\t"+get_yearvariance(kp.Key)+"\t"+get_countryvariance(kp.Key)+"\n");
            }
            return sb.ToString();
        }

        public void merge(hashcorpusclass hc)
        {
            foreach (int h in hc.hashwords)
                this.hashwords.Add(h);
            foreach (int h in hc.wordfreqdict.Keys)
            {
                if (this.wordfreqdict.ContainsKey(h))
                    this.wordfreqdict[h] += hc.wordfreqdict[h];
                else
                    this.wordfreqdict.Add(h, hc.wordfreqdict[h]);
            }
            this.totalwords += hc.totalwords;
        }

        public void yearcountrymerge()
        {
            if (!yearcorpus.ContainsKey(this.year))
            {
                yearcorpus.Add(this.year, new hashcorpusclass());
                yearcorpus[this.year].year = this.year;
            }
            yearcorpus[this.year].merge(this);

            if (!countrycorpus.ContainsKey(this.countrycode))
            {
                countrycorpus.Add(this.countrycode, new hashcorpusclass());
                countrycorpus[this.countrycode].countrycode = this.countrycode;
            }
            countrycorpus[this.countrycode].merge(this);

        }

        public static double get_yearvariance(int nw)
        {
            double[] dd = new double[yearcorpus.Count];

            int i = 0;
            foreach (int year in yearcorpus.Keys)
            {
                if (yearcorpus[year].wordfreqdict.ContainsKey(nw))
                {
                    dd[i] = yearcorpus[year].wordfreqdict[nw];
                }
                else
                    dd[i] = 0;
                i++;
            }

            return dd.Variance();
        }

        public static double get_countryvariance(int nw)
        {
            double[] dd = new double[countrycorpus.Count];

            int i = 0;
            foreach (string country in countrycorpus.Keys)
            {
                if (countrycorpus[country].wordfreqdict.ContainsKey(nw))
                {
                    dd[i] = countrycorpus[country].wordfreqdict[nw];
                }
                else
                    dd[i] = 0;
                i++;
            }

            return dd.Variance();
        }

        public void ngram_cutoff(int mincount)
        {
            Dictionary<string, int> tempdict = new Dictionary<string, int>();
            foreach (KeyValuePair<string,int> kp in ngramcountdict)
            {
                if (kp.Value >= mincount)
                    tempdict.Add(kp.Key,kp.Value);
            }
            ngramcountdict.Clear();
            ngramcountdict = tempdict;
        }

        public void find_ngrams(int n)
        {
            find_ngrams(n, false);
        }

        public void find_ngrams(int n, bool checkmaster)
        {
            char[] chars = new char[2*n];
            for (int i=0;i<hashwords.Count- n;i++)
            {
                for (int j=0;j< n;j++)
                {

                    chars[2*j] = (char)(hashwords[i + j] & 0x0000FFFF);
                    chars[2 * j + 1] = (char)(hashwords[i + j] >> 16);
                }
                string nstring = new string(chars);
                if (checkmaster)
                    if (!mastercorpus.ngramcountdict.ContainsKey(nstring))
                        continue;
                if (ngramcountdict.ContainsKey(nstring))
                    ngramcountdict[nstring]++;
                else
                    ngramcountdict.Add(nstring, 1);
            }
        }

        public static string decode_ngram(string s)
        {
            StringBuilder sbf = new StringBuilder("");
            for (int j = 0; j < s.Length; j += 2)
            {
                int n = (int)s[j] + ((int)s[j + 1] << 16);
                sbf.Append(hashtoworddict[n] + " ");
            }
            return sbf.ToString();
        }

        public string print_ngrams(int nmin)
        {
            StringBuilder sb = new StringBuilder("\n\n==============================\n"+this.countrycode + "\t" + this.year + "\n");
            foreach (string s in ngramcountdict.Keys)
            {
                if (ngramcountdict[s] >= nmin)
                {
                    string ng = decode_ngram(s);
                    sb.Append(ng.ToString() + "\t" + ngramcountdict[s] + "\n");
                }
            }
            sb.Append("# different n-grams:\t" + ngramcountdict.Count + "\n");
            sb.Append("# words n-grams:\t" + ngramcountdict.Sum(x => x.Value).ToString());
            return sb.ToString();
        }
    
    //    public static int addngram(int[] hwords)
    //    {
    //        int h =  //hwords.GetHashCode(); Doesn't work with arrays

    //    }
    }
}
