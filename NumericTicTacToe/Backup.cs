//using System;
//using System.Collections.Generic;
//using System.IO;

//// Move class
//class Move
//{
//    public int Value { get; set; }
//    public (int X, int Y) Position { get; set; }
//    public Player Player { get; set; }
//    public string Timestamp { get; set; }
//    public bool IsValid { get; set; }
//}

//// Player class
//abstract class Player
//{
//    public string Name { get; set; }
//    public char PlayerType { get; set; }
//    public abstract Move MakeMove(Board board);
//}

//// Human Player
//class HumanPlayer : Player
//{
//    public override Move MakeMove(Board board)
//    {
//        int row, col, value;
//        while (true)
//        {
//            try
//            {
//                Console.WriteLine($"{Name}, it's your turn.");
//                Console.Write("Enter Row (0-2): ");
//                while (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row > 2)
//                    Console.Write("Invalid row. Enter again (0-2): ");

//                Console.Write("Enter Column (0-2): ");
//                while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col > 2)
//                    Console.Write("Invalid column. Enter again (0-2): ");

//                Console.Write("Enter Value (1-9): ");
//                while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 9)
//                    Console.Write("Invalid value. Enter again (1-9): ");

//                if (!board.ValidateMove(row, col, value))
//                {
//                    Console.WriteLine("Invalid move. The cell is already occupied. Try again.");
//                    continue;
//                }

//                return new Move { Position = (row, col), Value = value, Player = this, Timestamp = DateTime.Now.ToString(), IsValid = true };
//            }
//            catch
//            {
//                Console.WriteLine("Invalid input. Please enter valid numbers.");
//            }
//        }
//    }
//}

//// Computer Player
//class ComputerPlayer : Player
//{
//    private Random rand = new Random();
//    public override Move MakeMove(Board board)
//    {
//        int row, col, value;
//        do
//        {
//            row = rand.Next(0, 3);
//            col = rand.Next(0, 3);
//            value = (PlayerType == 'O') ? rand.Next(1, 10) | 1 : rand.Next(1, 10) & ~1;
//        } while (!board.ValidateMove(row, col, value));

//        Console.WriteLine($"Computer ({Name}) plays: {row} {col} {value}");
//        return new Move { Position = (row, col), Value = value, Player = this, Timestamp = DateTime.Now.ToString(), IsValid = true };
//    }
//}

//// UndoRedoManager class
//class UndoRedoManager
//{
//    private Stack<Move> undoStack = new Stack<Move>();
//    private Stack<Move> redoStack = new Stack<Move>();

//    public void RecordMove(Move move)
//    {
//        undoStack.Push(move);
//        redoStack.Clear();
//    }

//    public Move Undo()
//    {
//        if (undoStack.Count > 0)
//        {
//            Move move = undoStack.Pop();
//            redoStack.Push(move);
//            return move;
//        }
//        return null;
//    }

//    public Move Redo()
//    {
//        if (redoStack.Count > 0)
//        {
//            Move move = redoStack.Pop();
//            undoStack.Push(move);
//            return move;
//        }
//        return null;
//    }
//}

//// Board class
//class Board
//{
//    private int[,] cells = new int[3, 3];

//    public bool ValidateMove(int row, int col, int value)
//    {
//        return cells[row, col] == 0;
//    }

//    public void ApplyMove(Move move)
//    {
//        cells[move.Position.X, move.Position.Y] = move.Value;
//        DisplayBoard(); // Display board after every move
//    }

//    public bool CheckWin()
//    {
//        for (int i = 0; i < 3; i++)
//        {
//            if (cells[i, 0] + cells[i, 1] + cells[i, 2] == 15 || cells[0, i] + cells[1, i] + cells[2, i] == 15)
//                return true;
//        }
//        if (cells[0, 0] + cells[1, 1] + cells[2, 2] == 15 || cells[0, 2] + cells[1, 1] + cells[2, 0] == 15)
//            return true;
//        return false;
//    }
//    public void DisplayBoard()
//    {
//        Console.WriteLine("Current Game Board:");
//        for (int i = 0; i < 3; i++)
//        {
//            for (int j = 0; j < 3; j++)
//            {
//                Console.Write(cells[i, j] == 0 ? "-\t" : cells[i, j] + "\t");
//            }
//            Console.WriteLine();
//        }
//        Console.WriteLine();
//    }
//}

//// Game class
//class Game
//{
//    private Board board = new Board();
//    private UndoRedoManager undoRedoManager = new UndoRedoManager();
//    private List<Player> players;
//    private int currentPlayerIndex = 0;

//    public Game(bool humanVsHuman)
//    {
//        players = new List<Player>
//        {
//            new HumanPlayer { Name = "Player 1", PlayerType = 'O' },
//            humanVsHuman ? new HumanPlayer { Name = "Player 2", PlayerType = 'E' } : new ComputerPlayer { Name = "Computer", PlayerType = 'E' }
//        };
//    }

//    public void StartGame()
//    {
//        board.DisplayBoard();
//        while (true)
//        {
//            Player currentPlayer = players[currentPlayerIndex];
//            Move move = currentPlayer.MakeMove(board);
//            if (move.IsValid)
//            {
//                board.ApplyMove(move);
//                undoRedoManager.RecordMove(move);

//                if (board.CheckWin())
//                {
//                    Console.WriteLine($"{currentPlayer.Name} won!");
//                    break;
//                }
//                currentPlayerIndex = 1 - currentPlayerIndex;
//            }
//        }
//    }
//}

//class Backup
//{
//    static void Main()
//    {
//        Console.WriteLine("Choose Game Mode:");
//        Console.WriteLine("1. Human vs Human");
//        Console.WriteLine("2. Human vs Computer");
//        string choice = Console.ReadLine();
//        Game game = new Game(choice == "1");
//        game.StartGame();
//    }
//}
