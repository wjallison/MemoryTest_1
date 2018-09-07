using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MemoryTest_1
{
    class ClassicNetwork
    {
        List<Matrix<double>> weights = new List<Matrix<double>>();
        List<Matrix<double>> biases = new List<Matrix<double>>();
        int numLayers;


        public ClassicNetwork(List<int> sizes) // Example input: { 784, 30, 10 }
        {
            numLayers = sizes.Count();

            for (int i = 1; i < numLayers; i++)
            {
                biases.Add(Matrix<double>.Build.Random(sizes[i], 1));
            }

            for (int i = 1; i < numLayers; i++)
            {
                weights.Add(Matrix<double>.Build.Random(sizes[i], sizes[i - 1]));
            }

        }


        public Matrix<double> feedForward(Matrix<double> a)
        {
            for (int i = 0; i < biases.Count(); i++)
            {
                for (int j = 0; j < weights.Count(); j++)
                {
                    a = sigmoid(weights[i] * a + biases[j]);
                }
            }

            return a;
        }

        public double sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Pow(2.71828, -z));
        }
        public double sigmoidPrime(double z)
        {
            return sigmoid(z) * (1 - sigmoid(z));
        }

        public Matrix<double> sigmoid(Matrix<double> z)
        {
            Matrix<double> result = z;
            for (int i = 0; i < z.RowCount; i++)
            {
                result[i, 0] = sigmoid(z[i, 0]);
            }

            return result;
        }
        public Matrix<double> sigmoidPrime(Matrix<double> z)
        {
            Matrix<double> result = z;
            for (int i = 0; i < z.RowCount; i++)
            {
                result[i, 0] = sigmoidPrime(z[i, 0]);
            }
            return result;
        }

        public void SGD(List<List<Matrix<double>>> trainingData, int reps, int miniBatchSize,
            double N, List<Matrix<double>> testData = null)
        {
            int nTest = 0;
            List<List<List<Matrix<double>>>> miniBatches = new List<List<List<Matrix<double>>>>();

            if (testData != null)
            {
                nTest = testData.Count();
            }

            int n = trainingData.Count();
            for (int i = 0; i < reps; i++)
            {
                //init mini batches
                Random rand = new Random();

                List<List<Matrix<double>>> miniBatch = new List<List<Matrix<double>>>();
                for (int j = 0; j < miniBatchSize; j++)
                {
                    miniBatch.Add(trainingData[rand.Next(0, n - 1)]);
                }

                miniBatches.Add(miniBatch);

            }

            for (int i = 0; i < reps; i++)
            {
                updateMiniBatch(miniBatches[i], N);
            }
        }

        public void updateMiniBatch(List<List<Matrix<double>>> miniBatch, double N)
        {
            List<Matrix<double>> nabla_b = new List<Matrix<double>>();
            List<Matrix<double>> nabla_w = new List<Matrix<double>>();

            List<Matrix<double>> d_nabla_b = new List<Matrix<double>>();
            List<Matrix<double>> d_nabla_w = new List<Matrix<double>>();
            for (int i = 0; i < biases.Count(); i++)
            {
                nabla_b.Add(Matrix<double>.Build.Dense(biases[i].RowCount, 1));
            }
            for (int i = 0; i < weights.Count(); i++)
            {
                nabla_w.Add(Matrix<double>.Build.Dense(weights[i].RowCount, weights[i].ColumnCount));
            }


            //Acquire gradients
            for (int i = 0; i < miniBatch.Count(); i++)
            {
                List<List<Matrix<double>>> temp = backProp(miniBatch[i][0], miniBatch[i][1]);
                d_nabla_b = temp[0];
                d_nabla_w = temp[1];

                for (int j = 0; j < nabla_b.Count(); j++)
                {
                    nabla_b[j] = nabla_b[j] + d_nabla_b[j];
                }
                for (int j = 0; j < nabla_w.Count(); j++)
                {
                    nabla_w[j] = nabla_w[j] + d_nabla_w[j];
                }
            }

            for (int i = 0; i < weights.Count(); i++)
            {
                weights[i] = weights[i] - (N / miniBatch.Count()) * nabla_w[i];
            }
            for (int i = 0; i < biases.Count(); i++)
            {
                biases[i] = biases[i] - (N / miniBatch.Count()) * nabla_b[i];
            }
        }


        public List<List<Matrix<double>>> backProp(Matrix<double> x, Matrix<double> y)
        {
            List<Matrix<double>> activations = new List<Matrix<double>>();
            Matrix<double> activation = x;
            activations.Add(x);
            List<Matrix<double>> zs = new List<Matrix<double>>();

            List<Matrix<double>> nambla_b = biases;
            List<Matrix<double>> nambla_w = weights;

            //Forward
            for (int i = 0; i < numLayers - 1; i++)
            {
                Matrix<double> z = weights[i] * activation + biases[i];
                zs.Add(z);
                activation = sigmoid(z);
                activations.Add(activation);
            }

            //Backward

            Matrix<double> del = Hadamard((activations[activations.Count() - 1] - y), sigmoidPrime(zs[zs.Count() - 1]));

            nambla_b[nambla_b.Count() - 1] = del;
            nambla_w[nambla_w.Count() - 1] = del * activations[activations.Count() - 2].Transpose();

            for (int l = 2; l < numLayers; l++)
            {
                Matrix<double> z = zs[zs.Count() - l];
                Matrix<double> sp = sigmoidPrime(z);
                del = weights[weights.Count() - l + 1].Transpose() * del;
                del = Hadamard(del, sp);
                nambla_b[nambla_b.Count() - l] = del;
                nambla_w[nambla_w.Count() - l] = del * activations[activations.Count() - l - 1].Transpose();
            }

            List<List<Matrix<double>>> res = new List<List<Matrix<double>>>();
            res.Add(nambla_b);
            res.Add(nambla_w);
            return res;
        }
        public Matrix<double> Hadamard(Matrix<double> slf, Matrix<double> other)
        {
            Matrix<double> res = slf;
            for (int i = 0; i < slf.RowCount; i++)
            {
                for (int j = 0; j < slf.ColumnCount; j++)
                {
                    res[i, j] = slf[i, j] * other[i, j];
                }
            }
            return res;
        }
    }
}
