using System;
using System.Collections.Generic;
using System.Linq;

namespace WtiOil
{
    /// <summary>
    /// <c>Regression</c> - Класс, обеспечивающий нахождение коэффициентов полиномиальной регресии.
    /// </summary>
    class Regression
    {
        // Выборки значений Х и Y.
        private readonly double[] sampleX, sampleY;

        // Количество элементов в выборке. 
        private readonly int length;

        // Степень полинома.
        private byte polynomialDegree;

        // Среднеквадратичная погрешность.
        private readonly double error;           

        // Правая и левая части СЛАУ.
        private double[] linearSystemRightSide;
        private double[,] linearSystemMatrix;

        /// Коэффициенты полиномов.
        public double[] coefficients;

        /// <summary>
        /// <c>Regression</c> - Класс, обеспечивающий нахождение коэффициентов полиномиальной регресии.
        /// </summary>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        /// <param name="error">Допустимая погрешность</param>
        private Regression(double[] xPoints, double[] yPoints, double error)
            :this(0,xPoints, yPoints)
        {
            if (error <= 0)
                throw new FormatException("Значение ошибки должно быть больше нуля");
            this.error = error;
        }

        /// <summary>
        /// <c>Regression</c> - Класс, обеспечивающий нахождение коэффициентов полиномиальной регресии.
        /// </summary>
        /// <param name="polynomialDegree">Степень полинома</param>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        private Regression(byte polynomialDegree, double[] xPoints, double[] yPoints)
	    {
            if (xPoints.Length != yPoints.Length)
                throw new Exception("Размеры массивов Х и У должны совпадать");
            this.sampleX = xPoints;
            this.sampleY = yPoints;
            this.length = xPoints.Length;
            this.polynomialDegree = ++polynomialDegree;
	    }

        /// <summary>
        /// <c>Regression</c> - Класс, обеспечивающий нахождение коэффициентов регрессии
        /// </summary>
        private Regression()
        {
        
        }

        /// <summary>
        /// Производит расчеты и возвращает массив коэффициентов полинома.
        /// </summary>
        /// <returns>Массив коэффициентов полинома</returns>
        private double[] GetCoefficients()
        {
            // Инициализация.
            Initialize();

            // Рассчет коеффициентов.
            CalculatePolynomialCoefficients();

            // Проверка на среднеквадратичную погрешность :
            //  Если погрешность меньше или равена заданной - возвращать значение результатов.

            if (CalculateError() <= error || error == 0.0)
                return coefficients;

            if (polynomialDegree >= 255)
                throw new Exception("Невозможно получить коэффициенты полинома с заданной погрешностью");

            // Если погрешность больше заданной - увеличивать степень полинома и рекурсивно вызывать метод.
            polynomialDegree++;
            return GetCoefficients();
        }

        /// <summary>
        /// Инициализация массивов.
        /// </summary>
        private void Initialize()
        {
            linearSystemRightSide = new double[polynomialDegree];
            coefficients = new double[polynomialDegree];
            linearSystemMatrix = new double[polynomialDegree, polynomialDegree];            
        }
        
        /// <summary>
        /// Производит формирование матрицы и решение ее методом Гаусса.
        /// </summary>                
        private void CalculatePolynomialCoefficients()
        {

            // Коеффициенты полиномов в СЛАУ. 
            // С[j] == Σ Pow(x[i], j), j = 0,1,2 ... polynomialDegree*2.
            double[] C = new double[2 * polynomialDegree - 1];

            // заполняем С и и правую часть СЛАУ.
            for (int i = 0; i < length; i++)
            {
                double f = 1, tmp = sampleY[i];
                for (int j = 0; j < 2 * polynomialDegree-1; j++)
                {
                    if (j < polynomialDegree)
                    {
                        linearSystemRightSide[j] += tmp;
                        tmp *= sampleX[i];
                    }
                    C[j] += f;
                    f *= sampleX[i];
                }
            }

            // Формируем матрицу левой части СЛАУ.
            for (int i = 0; i < polynomialDegree; i++)
            {
                int k = i;
                for (int j = 0; j < polynomialDegree; j++)
                    linearSystemMatrix[i,j] = C[k++];                               
            }

            coefficients = SolveMatrix(polynomialDegree, linearSystemMatrix, linearSystemRightSide);
        }
        
        /// <summary>
        /// Производит решение матрицы методом Гаусса с выбором главного элемента.
        /// </summary>
        private double[] SolveMatrix(int polynomDegree, double[,] leftSide, double[] rightSide)
        {
            double[] coeffs = new double[polynomDegree];

            for (int i = 0; i < polynomDegree - 1; i++)
            {
                // Поиск главного элемента в столбце.
                double max = Math.Abs(leftSide[i, i]);
                int index = i;

                for (int j = i + 1; j < polynomDegree; j++)
                {
                    if (Math.Abs(leftSide[j, i]) > max)
                    {
                        max = Math.Abs(leftSide[j, i]);
                        index = j;
                    }
                }

                // Поднимаем строку с главным элементом вверх.
                if (index > i)
                {
                    Invert(ref rightSide[index], ref rightSide[i]);

                    for (int j = 0; j < polynomDegree; j++)
                        Invert(ref leftSide[index, j], ref leftSide[i, j]);
                }

                // Вычисление масштабирующих множителей.
                for (int j = i + 1; j < polynomDegree; j++)
                {
                    // Если главный элемент - ноль, переход на следующую итерацию итерацию
                    if (leftSide[i, i] == 0)
                        continue;

                    double tmp = leftSide[j, i] / leftSide[i, i];

                    for (int k = i; k < polynomDegree; k++)
                        leftSide[j, k] -= leftSide[i, k] * tmp;

                    rightSide[j] -= rightSide[i] * tmp;
                }
            }

            // Итоговое решение СЛАУ.
            for (int i = polynomDegree - 1; i >= 0; i--)
            {
                coeffs[i] = rightSide[i];

                for (int j = polynomDegree - 1; j > i; j--)
                    coeffs[i] -= leftSide[i, j] * coeffs[j];

                if (leftSide[i, i] == 0)
                {
                    if (rightSide[i] == 0)
                        throw new Exception("СЛАУ имеет множество решений");
                    else
                        throw new Exception("СЛАУ не имеет решений");
                }

                coeffs[i] /= leftSide[i, i];
            }

            return coeffs;
        }

        /// <summary>
        /// Формирует массив значений y*(X), 
        /// где y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </summary>
        /// <returns>Массив значений y*(X)</returns>
        private double[] GetYFromXValue()
        {
            return GetYFromXValue(coefficients, sampleX);
        }

        /// <summary>
        /// Возвращает среднеквадратичную погрешность.
        /// </summary>
        /// <returns>Среднеквадратичная погрешность</returns>
        private double CalculateError()
        {
            return GetError(sampleY, GetYFromXValue());
        }

        /// <summary>
        /// Меняет местами значения переменных<c>a</c> и <c>b</c>.
        /// </summary>
        /// <param name="a">Значение первой переменной</param>
        /// <param name="b">Значение второй переменной</param>
        private void Invert(ref double a, ref double b)
        {
            
            double temp;

            temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Возвращает результат суммы произведений элементов массивов x и y
        /// </summary>
        /// <param name="x">Массив элементов</param>
        /// <param name="y">Массив элементов</param>
        /// <returns>Сумма произведений элементов</returns>
        private double GetSumOfMultiplication(double[] x, double[] y)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
                sum += x[i] * y[i];

            return sum;
        }

        /// <summary>
        /// Многофакторная регрессия. Возвращает массив коэффициентов полинома y*(x1,x2)
        /// </summary>
        /// <param name="y">Массив значений зависимой переменной многофакторной регрессии</param>
        /// <param name="x1">Массив значений независимой переменной многофакторной регрессии</param>
        /// <param name="x2">Массив значений независимой переменной многофакторной регрессии</param>
        /// <returns>Массив коэффициентов полинома y*(x1,x2)</returns>
        private double[] CalculateMultipleRegression(double[] y, double[] x1, double[] x2)
        {
            double[,] leftSide = {{y.Length, x1.Sum(), x2.Sum()},
                                 {x1.Sum(), x1.Sum(z => z * z),GetSumOfMultiplication(x1, x2)},
                                 {x2.Sum(),GetSumOfMultiplication(x1, x2),x2.Sum(z => z * z)}};

            double[] rightSide = { y.Sum(), GetSumOfMultiplication(x1, y), GetSumOfMultiplication(x2, y) };

            return SolveMatrix(3, leftSide, rightSide);
        }

        /// <summary>
        /// Многофакторная регрессия. Возвращает массив коэффициентов полинома y*(x1,x2)
        /// </summary>
        /// <param name="y">Массив значений зависимой переменной многофакторной регрессии</param>
        /// <param name="x1">Массив значений независимой переменной многофакторной регрессии</param>
        /// <param name="x2">Массив значений независимой переменной многофакторной регрессии</param>
        /// <returns>Массив коэффициентов полинома y*(x1,x2)</returns>
        public static double[] GetMultipleRegressionCoefficients(double[] y, double[] x1, double[] x2)
        {
            return new Regression().CalculateMultipleRegression(y, x1, x2);
        }

        /// <summary>
        /// Возвращает массив коэффициентов полинома y*(X) с допустимой погрешностью.
        /// </summary>
        /// <remarks>
        /// y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </remarks>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        /// <param name="error">Допустимая погрешность</param>
        /// <returns>Массив коэффициентов полинома</returns>
        public static double[] GetCoefficients(double[] xPoints, double[] yPoints, double error)
        {
            return new Regression(xPoints, yPoints, error).GetCoefficients();
        }

        /// <summary>
        /// Возвращает массив коэффициентов полинома y*(X) с заданной степенью полинома.
        /// </summary>
        /// <remarks>
        /// y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </remarks>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        /// <param name="polynomialDegree">Степень полинома</param>
        /// <returns>Массив коэффициентов полинома</returns>
        public static double[] GetCoefficients(double[] xPoints, double[] yPoints, byte polynomialDegree)
        {
            return new Regression(polynomialDegree, xPoints, yPoints).GetCoefficients();
        }

        /// <summary>
        /// Возвращает массив значений y*(<c>x1</c>,<c>x2</c>), при коэффициентах <c>coefficients</c>.
        /// </summary>
        /// <remarks>
        /// y*(x1,x2) = a[0] + a[1]*x1 + a[2]*x2.
        /// </remarks>
        /// <param name="coefficients">Массив коэффициентов регрессии</param>
        /// <param name="x1">Массив значений независимой переменной многофакторной регрессии</param>
        /// <param name="x2">Массив значений независимой переменной многофакторной регрессии</param>
        /// <returns></returns>
        public static double[] GetMultipleYFromXValue(double[] coefficients, double[] x1, double[] x2)
        { 
            double[] y = new double[x1.Length];

            for (int i = 0; i < x1.Length; i++)
                y[i] = coefficients[0] + coefficients[1] * x1[i] + coefficients[2] * x2[i]; 

            return y;
        }

        /// <summary>
        /// Возвращает массив значений y*(<c>SampleX</c>), при коэффициентах <c>coefficients</c>.
        /// </summary>
        /// <remarks>
        /// y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </remarks>
        /// <param name="coefficients">Массив коэффициентов полинома</param>
        /// <param name="sampleX">Массив значений Х</param>
        /// <returns>Массив значений y*(Х)</returns>
        public static double[] GetYFromXValue(double[] coefficients, double[] sampleX)
        {
            double[] y = new double[sampleX.Length];

            for (int j = 0; j < sampleX.Length; j++)
                for (int i = 0; i < coefficients.Length; i++)
                    y[j] += coefficients[i] * Math.Pow(sampleX[j], i);

            return y;
        }

        /// <summary>
        /// Рассчитывает коэффициенты полинома и возвращает массив значений y*(<c>xPoints</c>).
        /// </summary>
        /// <remarks>
        /// y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </remarks>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        /// <param name="error">Допустимая погрешность</param>
        /// <returns>Массив значений y*(Х)</returns>
        public static double[] GetYFromXValue(double[] xPoints, double[] yPoints, double error)
        {
            var polinom = new Regression(xPoints, yPoints, error);
            polinom.GetCoefficients();
            return polinom.GetYFromXValue();
        }

        /// <summary>
        /// Рассчитывает коэффициенты полинома и возвращает массив значений y*(<c>xPoints</c>).
        /// </summary>
        /// <remarks>
        /// y*(X) = a[0] + a[1]*x + a[2]*x^2 + ... + a[m]*x^m.
        /// </remarks>
        /// <param name="xPoints">Массив точек Х</param>
        /// <param name="yPoints">Массив точек Y(Х)</param>
        /// <param name="polynomialDegree">Степень полинома</param>
        /// <returns>Массив значений y*(Х)</returns>
        public static double[] GetYFromXValue(double[] xPoints, double[] yPoints, byte polynomialDegree)
        {
            var polinom = new Regression(polynomialDegree, xPoints, yPoints);
            polinom.GetCoefficients();
            return polinom.GetYFromXValue();
        }

        /// <summary>
        /// Возвращает среднеквадратичную погрешность.
        /// </summary>
        /// <param name="yPoints">Массив исходных точек</param>
        /// <param name="polynomY">Массив полученных при полиномиальной регрессии точек</param>
        /// <returns>Среднеквадратичная погрешность</returns>
        public static double GetError(double[] yPoints, double[] polynomY)
        {
            double error = 1.0 / (yPoints.Length + 1);
            double sum = 0;
            for (int i = 0; i < yPoints.Length; i++)
            {
                sum += Math.Pow(yPoints[i] - polynomY[i], 2);
            }

            return Math.Sqrt(error * sum);
        }
    }
}
