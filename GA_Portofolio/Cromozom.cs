using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace GA_Portofolio
{
    public class Cromozom:IComparable
    {
        public int Length;
        public float CurrentFitness;
        public float CurrentVenit;
        private int CrossoverPoint;
        private int IndiceFitness=1;

         ArrayList TheArray = new ArrayList(); //array cu date
         public static Random Rand = new Random((int)DateTime.Now.Ticks);

         public Cromozom()
         {
         }

         public Cromozom(int length, int indf)//constructor 
         {
             Length = length;
             IndiceFitness = indf;

             for (int i = 0; i < Length; i++)
             {
                 float nextValue = GenerateGeneValue();
                 TheArray.Add(nextValue);
             }
         }

         public int CompareTo(object c) //functie de comparatie
         {
             //return (Math.Sign(this.CurrentFitness - ((Cromozom)c1).CurrentFitness));
             if ((((Cromozom)c).CurrentFitness - this.CurrentFitness) > 0)
                 return 1;
             else  return -1;
            // return (Math.Sign(((Cromozom)c).CurrentFitness - this.CurrentFitness));
         }

         private float FitnessMax()//maximizarea profitului
         {
             float x, sum_yax = 0, sumx = 0;
             for (int i = 0; i < Containerr.count; i++)
             {
                 x = (float)Convert.ToDouble(Containerr.Pret[i]) * (float)Math.Round(Convert.ToInt32(Containerr.CMin[i]) +
                 Math.Round((Convert.ToInt32(Containerr.CMax[i]) - Convert.ToInt32(Containerr.CMin[i])) * (float)(TheArray[i])));
                 sum_yax += (float)(x * Convert.ToDouble(Containerr.Venit[i]) / 100.0f);
                 sumx += x;
             }

             CurrentVenit = sum_yax / sumx;
             return sum_yax / sumx - (2 * Math.Abs(sumx - Containerr.summa) + (sumx - Containerr.summa)) /(Containerr.summa * 10);
         }

         private float FitnessMin()
         {
             return 1.0f;
         }

         private float FitnessMinMax()
         {
             return 1.0f;
         }

         public void CalculateFitness()
         {
             switch (IndiceFitness)
             {
                 case 1:
                     CurrentFitness = FitnessMax();
                     break;
                 case 2:
                     CurrentFitness = FitnessMin();
                     break;
                 case 3:
                     CurrentFitness = FitnessMinMax();
                     break;
                 default:
                     CurrentFitness = FitnessMax();
                     break;
             }

         }

         public float GenerateGeneValue() //generarea alelei
         {
             return (float)Rand.NextDouble();
         }
        
        public  void Mutatie() //mutatie
        {
            int AffectedGenes = Rand.Next(Length); //determinam cite gene trebuie mutate
            for (int i = 0; i < AffectedGenes; i++)
            {
                int Index = Rand.Next((int)Length);
                float val = GenerateGeneValue();
                TheArray[Index] = val;    
            }

        }

        public  string ToString()
        {
            string strResult = "";
            for (int i = 0; i < Length; i++)
            {
                strResult += this[i] + "  ";
            }
            return strResult;
        }

        public void CopyGeneInfo(Cromozom dest)
        {  
            dest.Length = Length;
        }

        public float this[int arrayindex]//pentru indixare
        {
            get
            {
                return (float)Convert.ToDouble(TheArray[arrayindex]);
            }
            set
            {
                TheArray[arrayindex] = value;
            }
        }

        public Cromozom ConvexCrossover(Cromozom g)
        {
            Cromozom aGene1 = new Cromozom();
            Cromozom aGene2 = new Cromozom();
            g.CopyGeneInfo(aGene1);
            g.CopyGeneInfo(aGene2);

            double a;
            float var;

            for (int i = 0; i < Length; i++)
            {
                a=Rand.NextDouble()*1.5-0.25; 
                var = (float)(a * (float)TheArray[i] + (1 - a) * (float)g.TheArray[i]);
                aGene1.TheArray.Add(var < 0.0f ? 0.0f : (var > 1.0f ? 1.0f : var));
                var = (float)((1 - a) * (float)TheArray[i] + a * (float)g.TheArray[i]);
                aGene2.TheArray.Add(var < 0.0f ? 0.0f : (var > 1.0f ? 1.0f : var));    
            }

            Cromozom aGene = null;
            if (Rand.Next(2) == 1)
            {
                aGene = aGene1;
            }
            else
            {
                aGene = aGene2;
            }

            return aGene;

        }

        public  Cromozom UniformCrossover(Cromozom g)
        {
            Cromozom aGene1 = new Cromozom();
            Cromozom aGene2 = new Cromozom();
            g.CopyGeneInfo(aGene1);
            g.CopyGeneInfo(aGene2);

            for (int i = 0; i < Length; i++)
            {
                if (Rand.Next(100) % 2 == 0)
                {
                    aGene1.TheArray.Add(g.TheArray[i]);
                    aGene2.TheArray.Add(TheArray[i]);
                }
                else
                {
                    aGene1.TheArray.Add(TheArray[i]);
                    aGene2.TheArray.Add(g.TheArray[i]);
                }

            }

            Cromozom aGene = null;
            if (Rand.Next(2) == 1)
            {
                aGene = aGene1;
            }
            else
            {
                aGene = aGene2;
            }

            return aGene;

        }

        public  void CopyGeneFrom(Cromozom src)//copierea genotipului
        {
            for (int i = 0; i < TheArray.Count; i++)
            {
                TheArray[i] = src.TheArray[i];
            }
            Length = src.Length;
            CurrentFitness = src.CurrentFitness;
            CurrentVenit = src.CurrentVenit;
        }
        
    }
}
