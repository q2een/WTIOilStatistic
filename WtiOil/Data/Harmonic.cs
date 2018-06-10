namespace WtiOil
{
    /// <summary>
    /// Предоставляет структуру, содержащий информацию о гармониках ряда Фурье.
    /// </summary>
    public struct Harmonic
    {   
        /// <summary>
        /// Частота гармоники.
        /// </summary>
        public double Frequency { get; private set; }
        
        /// <summary>
        /// Коэффициет Фурье.
        /// </summary>
        public double A { get; private set; }

        /// <summary>
        /// Коэффициет Фурье.
        /// </summary>
        public double B { get; private set; }

        /// <summary>
        /// Амплитуда гармоники.
        /// </summary>
        public double Аmplitude { get; private set; }

        /// <summary>
        /// Фаза гармоники.
        /// </summary>
        public double Phase { get; private set; }

        /// <summary>
        /// <c>Harmonic</c> - Структура описывающая параметры гармоники.
        /// </summary>
        /// <param name="frequency">Частота</param>
        /// <param name="a">Коэффициет Фурье</param>
        /// <param name="b">Коэффициет Фурье</param>
        /// <param name="amplitude">Амплитуда</param>
        /// <param name="phase">Фаза</param>
        public Harmonic(double frequency,double a, double b, double amplitude, double phase)
            : this()
        {
            this.Frequency = frequency;
            this.A = a;
            this.B = b;
            this.Аmplitude = amplitude;
            this.Phase = phase;
        }
    } 
}
