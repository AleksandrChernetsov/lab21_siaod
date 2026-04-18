using System.Diagnostics;

namespace lab21_siaod
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 8;      // число строк  
            dataGridView1.ColumnCount = 6;  // число столбцов 

            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            dataGridView1.Rows[7].Cells[1].Value = "Пирамидальная";

            dataGridView1.Rows[0].Cells[0].Value = false;
            dataGridView1.Rows[1].Cells[0].Value = false;
            dataGridView1.Rows[2].Cells[0].Value = false;
            dataGridView1.Rows[3].Cells[0].Value = true;
            dataGridView1.Rows[4].Cells[0].Value = true;
            dataGridView1.Rows[5].Cells[0].Value = true;
            dataGridView1.Rows[6].Cells[0].Value = true;
            dataGridView1.Rows[7].Cells[0].Value = true;
        }

        // Функция проверки упорядоченности массива по возрастанию 
        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }

        // Сортировка прямым обменом (пузырьковая) 
        private (int comparisons, int swaps, long time) BubbleSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            bool swapped;
            for (int i = 0; i < array.Length - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        swaps++;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка прямым выбором 
        private (int comparisons, int swaps, long time) SelectionSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    comparisons++;
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
                swaps++;
            }
            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка прямым включением (сортировка вставками с минимальным барьером) 
        private (long comparisons, long swaps, long time) InsertionSort(int[] array)
        {
            long comparisons = 0;
            long swaps = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int minIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                comparisons++;
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }
            }

            (array[0], array[minIndex]) = (array[minIndex], array[0]);
            swaps++;

            for (int i = 2; i < array.Length; i++)
            {
                int barrier = array[i];
                int j = i - 1;

                while (array[j] > barrier)
                {
                    comparisons++;
                    array[j + 1] = array[j];
                    swaps++;
                    j--;
                }
                comparisons++;

                array[j + 1] = barrier;
                swaps++;
            }

            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка Шелла 
        private (long comparisons, long swaps, long time) ShellSort(int[] array)
        {
            long comparisons = 0;
            long swaps = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = array.Length;

            int t = (int)Math.Floor(Math.Log(n, 2)) - 1;
            if (t < 1) t = 1;

            int h = (int)Math.Pow(2, t - 1);

            while (h >= 1)
            {
                for (int i = h; i < n; i++)
                {
                    int current = array[i];
                    int j = i;

                    while (j >= h && array[j - h] > current)
                    {
                        comparisons++;
                        array[j] = array[j - h];
                        swaps++;
                        j -= h;
                    }

                    array[j] = current;
                    swaps++;
                }

                h = h / 2;
            }

            stopwatch.Stop();
            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Быстрая сортировка (опорный элемент – левый) 
        private (int comparisons, int swaps, long time) QuickSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            QuickSortRecursive(array, 0, array.Length - 1, ref comparisons, ref swaps);
            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        private void QuickSortRecursive(int[] array, int left, int right, ref int comparisons, ref int swaps)
        {
            if (left >= right) return;

            int pivotIndex = Partition(array, left, right, ref comparisons, ref swaps);

            QuickSortRecursive(array, left, pivotIndex - 1, ref comparisons, ref swaps);
            QuickSortRecursive(array, pivotIndex + 1, right, ref comparisons, ref swaps);
        }

        private int Partition(int[] array, int left, int right, ref int comparisons, ref int swaps)
        {
            int pivot = array[left];
            int i = left;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                    if (i > right) break;
                    comparisons++;
                } while (i <= right && array[i] < pivot);

                do
                {
                    j--;
                    if (j < left) break;
                    comparisons++;
                } while (j >= left && array[j] > pivot);

                if (i >= j) break;
                (array[i], array[j]) = (array[j], array[i]);
                swaps++;
            }

            (array[left], array[j]) = (array[j], array[left]);
            swaps++;

            return j;
        }

        // Сортировка подсчетом (линейная сортировка) 
        private (long comparisons, long assignments, long time) CountingSort(int[] array)
        {
            long comparisons = 0;
            long assignments = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int n = array.Length;

            int max = array[0];
            for (int i = 1; i < n; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            int[] count = new int[max + 1];

            for (int i = 0; i < n; i++)
            {
                count[array[i]]++;
                assignments++;
            }

            int index = 0;
            for (int i = 0; i <= max; i++)
            {
                while (count[i] > 0)
                {
                    comparisons++;
                    array[index] = i;
                    assignments++;
                    index++;
                    count[i]--;
                }
            }

            stopwatch.Stop();

            return (comparisons, assignments, stopwatch.ElapsedMilliseconds);
        }

        // Функция ВНИЗ для восстановления пирамидальности
        private void Down(int[] a, int k, int n, ref long comps, ref long swaps)
        {
            while (2 * k + 1 < n)
            {
                int j = 2 * k + 1;

                if (j + 1 < n)
                {
                    comps++;
                    if (a[j] < a[j + 1]) j++;
                }

                comps++;
                if (a[k] >= a[j]) break;

                int temp = a[k];
                a[k] = a[j];
                a[j] = temp;
                swaps++;

                k = j;
            }
        }

        // Пирамидальная сортировка
        private (long comparisons, long swaps, long time) HeapSort(int[] array)
        {
            long comparisons = 0;
            long swaps = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Down(array, i, n, ref comparisons, ref swaps);
            }

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                swaps++;

                Down(array, 0, i, ref comparisons, ref swaps);
            }

            stopwatch.Stop();
            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Если галочка снята — стираем результаты 
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null || !(bool)dataGridView1.Rows[i].Cells[0].Value)
                {
                    dataGridView1.Rows[i].Cells[2].Value = null;
                    dataGridView1.Rows[i].Cells[3].Value = null;
                    dataGridView1.Rows[i].Cells[4].Value = null;
                    dataGridView1.Rows[i].Cells[5].Value = null;
                }
            }

            int size = (int)numericUpDown1.Value;
            int[] originalArray = new int[size];
            for (int i = 0; i < size; i++)
                originalArray[i] = random.Next(size);

            if ((bool)dataGridView1.Rows[0].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = BubbleSort(sortingArray);

                dataGridView1.Rows[0].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[0].Cells[3].Value = result.swaps;
                dataGridView1.Rows[0].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[0].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[1].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = SelectionSort(sortingArray);

                dataGridView1.Rows[1].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[1].Cells[3].Value = result.swaps;
                dataGridView1.Rows[1].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[1].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[2].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = InsertionSort(sortingArray);
                dataGridView1.Rows[2].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[2].Cells[3].Value = result.swaps;
                dataGridView1.Rows[2].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[2].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[3].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = ShellSort(sortingArray);

                dataGridView1.Rows[3].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[3].Cells[3].Value = result.swaps;
                dataGridView1.Rows[3].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[3].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[4].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = QuickSort(sortingArray);

                dataGridView1.Rows[4].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[4].Cells[3].Value = result.swaps;
                dataGridView1.Rows[4].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[4].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[5].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = CountingSort(sortingArray);

                dataGridView1.Rows[5].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[5].Cells[3].Value = result.assignments;
                dataGridView1.Rows[5].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[5].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[6].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Array.Sort(sortingArray);
                stopwatch.Stop();

                dataGridView1.Rows[6].Cells[2].Value = "-";
                dataGridView1.Rows[6].Cells[3].Value = "-";
                dataGridView1.Rows[6].Cells[4].Value = stopwatch.ElapsedMilliseconds + " мс";
                dataGridView1.Rows[6].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[7].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = HeapSort(sortingArray);

                dataGridView1.Rows[7].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[7].Cells[3].Value = result.swaps;
                dataGridView1.Rows[7].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[7].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}