# Sudoku Project

![image](https://github.com/user-attachments/assets/1cf84a93-93fe-4f68-aa88-72b4c4a19f7c)


A program that solve sudoku board, the program supports multiple board sizes (e.g., 9×9, 16×16, 25×25). It includes a **dynamic board** structure, a **solver** that uses heuristics and backtracking, **data validation**, and a **test suite** to ensure correctness.

## Key Features

- **Multiple Board Sizes**  
  Supports any (n×n) board where √n is an integer (e.g., 9×9, 16×16, 25×25).

- **Cell and Group Management**  
  Classes (Cell, CellGroup) for updating values and tracking possible options with bit masks.

- **Sudoku Solver**  
  Solves puzzles using various heuristics:
    -Naked Single
    -Hidden Single
    -Naked Pairs
    -All kind of Naked Combinations 
  Falls back to backtracking if needed.

- **Input Validation**  
  Ensures the puzzle string is the correct length and contains only valid characters.
  
-**Board Display**  
  Provides both a **string-based** representation of the board and a **simple graphical** representation.

- **Unit Tests**  
  Verifies logic for different board sizes, empty boards, unsolvable boards, and more.

---

## Data Structures and Classes

The project is organized into several folders under `src`, each handling a specific part of the Sudoku logic:

1. **SudokuBoard**  
   - **Board.cs**  
     Manages the 2D array of `Cell` objects (`cells[row, col]`) and parallel arrays of `CellGroup` (`rows`, `cols`, `cubes`).  
     Provides methods for setting/clearing values, recalculating possible options, and checking validity.

   - **Cell.cs**  
     Represents a single cell in the Sudoku grid.  
     Stores its current value (`0` if empty) and a bitmask of possible candidates.

   - **CellGroup.cs**  
     Represents a logical group (row, column, or box).  
     Holds references to multiple `Cell` objects in the group and tracks a combined mask of remaining candidates.

2. **Solver**  
   - **SudokuSolver.cs**  
     The main solver that attempts to solve a Sudoku board.  
     The solver uses a recursive strategy:It begins by applying various logical techniques,heuristics.
     If the puzzle remains unsolved after these heuristics, the solver use backtracking, it identifies the cell with the         fewest possible candidates, assigns one of the candidates, and recursively calls solve() again.
     
3. **Solver/SudokuRules**  
   Contains individual heuristic classes (e.g., `NakedSingle.cs`, `HiddenSingle.cs`, `NakedPairs.cs`), each implementing a     specific technique to fill cells or eliminate impossible candidates.

4. **Validation**  
   - **InputValidator.cs**  
     Verifies that the puzzle string (length, characters, sub-square size) is correct for the specified board.

5. **Exceptions**  
   Custom exceptions (e.g., `InvalidBoardException`, `UnsolveableBoardException`) for invalid or unsolvable puzzles.
   
---
## Installation

1. **Clone/Download the repository**  
   - Clone from GitHub:
   
        git clone https://github.com/AmitShech/sudoku.git

   - Or download it as a ZIP file and extract it locally.

2. **Open** the `Sudoku.sln` in Visual Studio.
3. **run** SudokuSolver
---

## Usage

There are two main ways to provide a puzzle string to the `Board`:

1. **Directly as a string**  
   For example, a 9×9 puzzle with 81 characters:
           "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
   
   <img width="660" alt="image" src="https://github.com/user-attachments/assets/a6adad52-20f1-4374-bab2-ed3d5d53647f" />

3. **From a text file**  
   Make sure the file containe one board:

       enter file name if it in the same folder, for example: "myPuzzle.txt"
       of the full path if it somewhere else.
   
   <img width="653" alt="image" src="https://github.com/user-attachments/assets/7578b3b5-2831-4f40-a443-edb932632a6d" />
   
---       

## Running Tests

1. **Open the Test Project**  
   Ensure that both the main project (`Sudoku.csproj`) and the test project (`SudokuTests.csproj`) are in the same solution.

2. **Run All Tests**  
   - In Visual Studio:  
     Open **Test Explorer** (menu: Test > Test Explorer), then click **Run All**.
     
   - Tests cover multiple scenarios:
     - Different board sizes (9×9, 16×16, 25×25,4x4)
     - Empty boards
     - Unsolvable boards
     - Performance checks

3. **View Results**  
   Passing tests appear in green, failing tests in red. Check any error messages for details on failures.

---
