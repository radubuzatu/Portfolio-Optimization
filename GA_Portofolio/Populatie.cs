using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace GA_Portofolio
{
    public class Populatie
    {
        public int kLength = 20;
        public int NumberOfGenerations = 100;
        public int NumberPopulation = 200;
        public float kMutationFrequency = 0.05f;
        public float  kCrossoverFrequency = 0.20f;
        private int IndiceFitness=1;

        public int Generation = 1;//arata a cita generatie
        bool Best2 = true ; //daca sa includem numai copii in generatie urmatoare sau cei mai buni
        public string Name = ""; //denumirea populatiei
        public bool FinishedRunning = false;

        // ArrayList Scores = new ArrayList();
        ArrayList Genomes = new ArrayList(); //populatii generate
        ArrayList GenomeReproducers = new ArrayList(); //genomuri care pot fi reprodusi
        ArrayList GenomeResults = new ArrayList(); //genomuri resultante
        ArrayList GenomeFamily = new ArrayList(); //se utilizeaza la crosover
        public ArrayList Rezultate = new ArrayList();
        public float Max; //la calcul fitnes
        public float Min; //la calcul fitnes

        public Populatie(string name,int klength,int np,int ng,float kmf,float kcf,int indice)
        {
            Name = name;
            kLength = klength;
            NumberOfGenerations = ng;
            NumberPopulation = np;
            kMutationFrequency = kmf;
            kCrossoverFrequency = kcf;
            IndiceFitness = indice;
            Rezultate.Clear();
            for (int i = 0; i < NumberPopulation; i++)
            {
                Cromozom crm = new Cromozom(kLength,IndiceFitness);
                crm.CalculateFitness();
                Genomes.Add(crm);
            }
 
        }

        public Populatie(string name) //initializarea populatiei initiale
        {
            Name = name;
            Rezultate.Clear();
            for (int i = 0; i < NumberPopulation; i++)
            {
                Cromozom crm = new Cromozom(kLength,IndiceFitness);
                crm.CalculateFitness();
                Genomes.Add(crm);
            }

        }

        private void Mutatie(Cromozom aGene) //Mutatii in dependenta de frecventa de mutare
        {
            if (Cromozom.Rand.Next(100) < (int)(kMutationFrequency * 100.0))
            {
                aGene.Mutatie();
            }
        }

        private void SelectCrossover(Cromozom aGene) //Crosover in dependenta de frecventa de crosover
        {
            if (Cromozom.Rand.Next(100) < (int)((aGene.CurrentFitness-Min)/(Max-Min)*100))
            { 
                GenomeReproducers.Add(aGene);
            }
        }

        public void NextGeneration(bool b)
        {
            // incrementam generatie;
            Generation++;
            
            GenomeReproducers.Clear();
            GenomeResults.Clear();
            Genomes.Sort();
            Max = ((Cromozom)Genomes[0]).CurrentFitness;//punct de control max
            Min = ((Cromozom)Genomes[Genomes.Count - 1]).CurrentFitness;//punct de control min
            // Rezultate.Add(((Cromozom)Genomes[0]).CurrentFitness);
            Rezultate.Add(((Cromozom)Genomes[0]).CurrentVenit);

            if (b) //alegerea pentru crosover dupa ordonare
            GenomeReproducers= new ArrayList(GetHighestScoreGenomes(Convert.ToInt32(Math.Round(Genomes.Count*kCrossoverFrequency))).ToList());
                /*
            for (int i = 0; i < Genomes.Count*kCrossoverFrequency; i++)
            {
                    GenomeReproducers.Add(Genomes[i]);
            }*/
            else         
            //alegerea pentu crosover probabilistic
             for (int i = 0; i < Genomes.Count; i++)
                 SelectCrossover((Cromozom)Genomes[i]);
  
            // efectuam crossingover si adaugam fii in populatie

            DoCrossover(GenomeReproducers);
            for (int i = 0; i < GenomeResults.Count; i++)
                Genomes.Add(GenomeResults[i]);

            // efectuam mutatie
            for (int i = 0; i < Genomes.Count; i++)
            {
                Mutatie((Cromozom)Genomes[i]);
            }

            // calculam fitness pentru cromozomi
            CalculateFitnessForAll();
            Genomes.Sort();
            
            // nimicim toate formele inplus
            if (Genomes.Count > NumberPopulation)
                Genomes.RemoveRange(NumberPopulation, Genomes.Count - NumberPopulation);
 
        }


        public void CalculateFitnessForAll() //calcularea fitnesului populatiei
        {
            foreach (Cromozom lg in Genomes)
            {
                lg.CalculateFitness();
            }
        }


        public void DoCrossover(ArrayList genes)
        {
            ArrayList GeneMoms = new ArrayList();
            ArrayList GeneDads = new ArrayList();

            for (int i = 0; i < genes.Count; i++)
            {
                // alegem Mama, Tata
                if (Cromozom.Rand.Next(100) % 2 > 0)
                {
                    GeneMoms.Add(genes[i]);
                }
                else
                {
                    GeneDads.Add(genes[i]);
                }
            }

            // egalam num M si T
            if (GeneMoms.Count > GeneDads.Count)
            {
                while (GeneMoms.Count > GeneDads.Count)
                {
                    GeneDads.Add(GeneMoms[GeneMoms.Count - 1]);
                    GeneMoms.RemoveAt(GeneMoms.Count - 1);
                }

                if (GeneDads.Count > GeneMoms.Count)
                {
                    GeneDads.RemoveAt(GeneDads.Count - 1); // num egal
                }

            }
            else
            {
                while (GeneDads.Count > GeneMoms.Count)
                {
                    GeneMoms.Add(GeneDads[GeneDads.Count - 1]);
                    GeneDads.RemoveAt(GeneDads.Count - 1);
                }

                if (GeneMoms.Count > GeneDads.Count)
                {
                    GeneMoms.RemoveAt(GeneMoms.Count - 1); // num egal
                }
            }

            // efectum crosover si adaugam
            for (int i = 0; i < GeneDads.Count; i++)
            {
                // alegem 2 forme cele mai bune intre parinti si copii
                Cromozom babyGene1 = null;
                Cromozom babyGene2 = null;

                int randomnum = Cromozom.Rand.Next(100) % 2;
                if (randomnum == 0)
                {
                    babyGene1 = ((Cromozom)GeneDads[i]).ConvexCrossover((Cromozom)GeneMoms[i]);
                    babyGene2 = ((Cromozom)GeneMoms[i]).ConvexCrossover((Cromozom)GeneDads[i]);
                }
                else  
                {
                    babyGene1 = ((Cromozom)GeneDads[i]).UniformCrossover((Cromozom)GeneMoms[i]);
                    babyGene2 = ((Cromozom)GeneMoms[i]).UniformCrossover((Cromozom)GeneDads[i]);
                }
               
                GenomeFamily.Clear();
                GenomeFamily.Add(GeneDads[i]);
                GenomeFamily.Add(GeneMoms[i]);
                GenomeFamily.Add(babyGene1);
                GenomeFamily.Add(babyGene2);

                CalculateFitnessForAll(GenomeFamily);

                for (int j = 0; j < 4; j++)
                {
                    CheckForUndefinedFitness((Cromozom)GenomeFamily[j]);//daca fitnesul e admisibil
                }

                GenomeFamily.Sort();

                if (Best2 == true)
                    //luam cei mai buni indivizi
                {                   
                    GenomeResults.Add(GenomeFamily[0]);
                    GenomeResults.Add(GenomeFamily[1]);
                }
                else
                {
                    GenomeResults.Add(babyGene1);
                    GenomeResults.Add(babyGene2);
                }

            }

        }

        public void CheckForUndefinedFitness(Cromozom g)
        {
            if (double.IsNaN(g.CurrentFitness))
                g.CurrentFitness = 0.01f;
        }

        public void CalculateFitnessForAll(ArrayList a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                ((Cromozom)a[i]).CalculateFitness(); 
            }
        }

        public Cromozom GetHighestScoreGenome()
        {
            Genomes.Sort();
            return (Cromozom)Genomes[0];
        }


        public Cromozom[] GetHighestScoreGenomes(int number)
        {
            Genomes.Sort();
            Cromozom[] strongestGenomes = new Cromozom[number];
            for (int i = 0; i < number; i++)
            {
                strongestGenomes[i] = new Cromozom(kLength,IndiceFitness);
                strongestGenomes[i].CopyGeneFrom((Cromozom)Genomes[i]);
            }
            return strongestGenomes;
        }


        public int Count
        {
            get
            {
                return Genomes.Count;
            }
        }

    }
}
