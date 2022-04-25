using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Sudoku
    {
      public bool validateSudoku(int[][] sudoku) //int N=3
        {
          

            if (isRegular(sudoku)) 
            {                

                int N = Convert.ToInt32( Math.Sqrt(sudoku.Length));

                //get horizontal lines
                IEnumerable<IEnumerable<int>> HorizontalLines =
                from line in sudoku select line;

                //get vertical lines
                IEnumerable<IEnumerable<int>> VerticalLines =
                from y in Enumerable.Range(0, N*N)
                select (
                    from x in Enumerable.Range(0, N*N)
                    select sudoku[x][y]);


                //check if it is matrix (regular square)
                if (VerticalLines.Count()==HorizontalLines.Count())
                {
                    IEnumerable<int> GetSquare(int x, int y) =>
                        from squareX in Enumerable.Range(0, N)
                        from squareY in Enumerable.Range(0, N)
                        select sudoku[x * N + squareX][y * N + squareY];

                    IEnumerable<IEnumerable<int>> Squares =
                        from x in Enumerable.Range(0, N)
                        from y in Enumerable.Range(0, N)
                        select GetSquare(x, y);
                    

                    if (VerticalLines.All(IsValid) && HorizontalLines.All(IsValid) && Squares.All(IsValid))
                        return true;
                    else
                        return false;
                }

                return false;                

            }

            return false;

        }

        bool IsValid(IEnumerable<int> line) => !(
                         from item in line
                         group item by item into g
                         where g.Count() > 1
                         select g).Any();

        bool isRegular(int[][] sudoku)
        {
            var list = sudoku.ToList();
            var tempLength = 0;
            var count = 1;
            foreach (var item in list)
            {
                if (tempLength == item.Length)
                {
                    count++;
                }
                else
                {
                    tempLength=item.Length;
                }

            }

            if (count == sudoku.Length)
                return true;
            else return false;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };

            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            int[][] badSudoku3 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4,5}
            };




            Sudoku s = new Sudoku();

            Console.WriteLine(s.validateSudoku(badSudoku3));
            Console.WriteLine(s.validateSudoku(goodSudoku1));
            Console.WriteLine(s.validateSudoku(goodSudoku2));
            Console.WriteLine(s.validateSudoku(badSudoku1));
            Console.WriteLine(s.validateSudoku(badSudoku2));           

            Console.ReadLine();

        }
    }
}
