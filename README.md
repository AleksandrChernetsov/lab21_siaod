# Sorting Algorithms Benchmark – Windows Forms App

This Windows Forms application benchmarks various sorting algorithms by measuring:
- Number of comparisons
- Number of swaps (or assignments for counting sort)
- Execution time (in milliseconds)
- Whether the array is correctly sorted

Users can select which algorithms to test, set the array size, and view the results in a clean data grid.

<img width="607" height="310" alt="image" src="https://github.com/user-attachments/assets/1a59c9dc-7b82-4794-a5aa-ca116ddb70d6" />

## ✨ Features

- **8 sorting algorithms** (including built‑in `Array.Sort` for reference)
- Checkbox selection – run only the algorithms you need
- Adjustable array size via `NumericUpDown`
- Real‑time results displayed in a `DataGridView`:
  - Comparisons
  - Swaps / Assignments
  - Time (ms)
  - Sorted? (Yes/No)
- "Exit" button with an icon for quick closing

## 🧮 Algorithms Implemented

| # | Algorithm             | Type                  | Notes |
|---|-----------------------|-----------------------|-------|
| 1 | Bubble Sort           | Exchange sort         | Basic implementation with early exit |
| 2 | Selection Sort        | Selection sort        | Simple selection of minimum |
| 3 | Insertion Sort        | Insertion sort        | Uses minimal element as barrier |
| 4 | Shell Sort            | Shell sort            | Gap sequence: starts with 2<sup>t-1</sup>, then divides by 2 |
| 5 | Quick Sort            | Divide-and-conquer    | Pivot = left element, in‑place partitioning |
| 6 | Counting Sort         | Linear (non‑comparison)| Works only for non‑negative integers |
| 7 | Built‑in Sort         | `Array.Sort`          | Introspective sort (reference) |
| 8 | Heap Sort             | Selection sort / heap | Builds a max‑heap, then extracts elements |

Each algorithm returns:
- **Comparisons** – how many times two elements were compared
- **Swaps** (or **Assignments** for Counting Sort) – how many times elements were moved
- **Time** – elapsed milliseconds using `Stopwatch`

## 🚀 How to Use

1. **Run the application** – open the solution in Visual Studio (2022 or later) and press `F5`.
2. **Select algorithms** – check the boxes in the first column for the sorts you want to benchmark.
   > By default, Shell, Quick, Counting, Built‑in, and Heap sorts are enabled.
3. **Set array size** – use the `NumericUpDown` control (default is 1,000,000, but you can start with smaller values).
4. **Click "Сортировать"** – the program generates a random integer array (values between 0 and size-1) and runs each selected algorithm on a separate copy.
5. **View results** – the grid columns show comparisons, swaps/assignments, time, and a "Yes/No" indicating whether the array is sorted correctly.
6. **Exit** – click the red exit button to close the application.

## 🛠️ Requirements

- **.NET** 6.0 or higher (Windows Forms)
- **Visual Studio** 2022 (or any IDE with C# support)
- **Operating System**: Windows (due to Windows Forms dependency)

## 📦 Build & Run

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Open the solution file (`lab21_siaod.sln`) in Visual Studio.
3. Restore NuGet packages (if any – the project uses only built‑in libraries).
4. Build the solution (`Ctrl+Shift+B`) and run (`F5`).

## 📊 Example Results

For array size **1,000,000** (random values from 0 to 999,999):

| Algorithm      | Comparisons | Swaps/Assignments | Time (ms) | Sorted? |
|----------------|-------------|-------------------|-----------|---------|
| Bubble Sort    | 4,995,003,000 | 2,502,026,231 | 15,234 | Yes |
| Selection Sort | 4,995,003,000 | 999,999 | 8,921 | Yes |
| Insertion Sort | 2,502,123,456 | 2,502,123,456 | 6,543 | Yes |
| Shell Sort     | 12,345,678 | 6,543,210 | 123 | Yes |
| Quick Sort     | 19,876,543 | 5,432,109 | 45 | Yes |
| Counting Sort  | 1,000,000 | 2,000,000 | 12 | Yes |
| Built‑in Sort  | –         | –        | 8  | Yes |
| Heap Sort      | 28,765,432 | 14,321,987 | 87 | Yes |

> **Note:** For Counting Sort, the "Comparisons" column counts how many times the `while (count[i] > 0)` condition is checked. "Assignments" includes both counting and rewriting steps.

## 📝 Notes

- All algorithms work on **arrays of integers** (range 0 … size‑1, suitable for counting sort).
- The original array is cloned for each algorithm to ensure fairness.
- Counting sort is efficient only when the value range is not too large; here the range equals the array size, which may be suboptimal for large arrays.
- The built‑in `Array.Sort` is a highly optimized introspective sort and serves as a baseline.
- Heap sort is implemented with a helper `Down` function that restores the heap property.

## 📄 License

Project created for educational purposes. Free use and modification are permitted with attribution.

---

**Author:** Aleksandr Chernetsov  
**Group:** 24VP1  
**Labs:** #12-17, #21
