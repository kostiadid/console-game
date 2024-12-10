using System;
using System.IO;
using System.Threading;

namespace MyConsoleApp
{
    internal class Program
    {
                static void Main(string[] args)
                {
                    char[,] map = ReadMap(@"C:\Program Files\dotnet\MyConsoleApp\bin\Debug\net8.0\map.txt");
                    ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
                    Console.CursorVisible = false;
                    Task.Run(() => {
                      while(true){
                    pressedKey = Console.ReadKey();
                      }
                    });
                    int pacmanX = 1;
                    int pacmanY = 1;
                                  // int previousPacmanX = pacmanX;
                                  // int previousPacmanY = pacmanY;
                    int score = 0;

                    int enemyX = 7;
                    int enemyY = 3;
                    int maxX = 5;
                    bool isMovingRight = true;
                    
                                int agentX = 35;
                                int agentY = 4;


          while (true)
        {
            Console.Clear();
                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);
            Console.ForegroundColor = ConsoleColor.Blue;
            DrawMap(map);

            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, map.GetLength(0)); 
            Console.Write($"Score: {score}");

            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, map.GetLength(0) + 1); 
            Console.Write($"Last Key: {pressedKey.Key}");

            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(enemyX, enemyY);
            Console.Write("P");
            if(isMovingRight)
            {
              enemyX++;
              if(enemyX >= maxX)
              {
                isMovingRight = false;
              }
            } else {enemyX--;
            if(enemyX <= 0)
            {
              isMovingRight = true;
            }
            }

              agentPath(pacmanX, pacmanY, ref agentX, ref agentY, map);
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.SetCursorPosition(agentX, agentY);
                        Console.Write("A");

                        if(pacmanX == agentX && pacmanY == agentY)
                        {      

                            Console.Clear();
                            Console.SetCursorPosition(15,0);
                            Console.WriteLine("yOu BeEn KeChEd By EnEmY ");
                            break;

                        }

            Thread.Sleep(200);

            if (Math.Abs(enemyX - pacmanX) <= 1 && Math.Abs(enemyY - pacmanY) <= 1)

            {
                
                Console.Clear();
                Console.SetCursorPosition(15,0);
                Console.WriteLine("yOu BeEn KeChEd By EnEmY ");
                break;
            }           
              }
    }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[file.Length, GetMaxLength(file)];

            for (int y = 0; y < file.Length; y++)
                for (int x = 0; x < file[y].Length; x++)
                    map[y, x] = file[y][x];
            return map;
        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.Write("\n");
            }
        }
        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionY, nextPacmanPositionX];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionY, nextPacmanPositionX] = ' ';
                }
            }
        }
        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
                direction[1] = -1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                direction[0] = 1;

            return direction;
        }
        private static int GetMaxLength(string[] lines)
        {
            int maxLength = lines[0].Length;
            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;
        }
                                                    private static void agentPath(int pacmanX, int pacmanY, ref int agentX, ref int agentY, char[,] map)
                                                    {
                                                        int newX = agentX;
                                                        int newY = agentY;

                                                        if (pacmanX > agentX && (agentX + 1) < map.GetLength(1) && map[agentY, agentX + 1] != '#')
                                                        {
                                                            newX = agentX + 1;
                                                        }
                                                        else if (pacmanX < agentX && (agentX - 1) >= 0 && map[agentY, agentX - 1] != '#')
                                                        {
                                                            newX = agentX - 1;
                                                        }
                                                        
                                                        if (pacmanY > agentY && (agentY + 1) < map.GetLength(0) && map[agentY + 1, agentX] != '#')
                                                        {
                                                            newY = agentY + 1;
                                                        }
                                                        else if (pacmanY < agentY && (agentY - 1) >= 0 && map[agentY - 1, agentX] != '#')
                                                        {
                                                            newY = agentY - 1;
                                                        }
                                                        if (newX >= 0 && newX < map.GetLength(1) && newY >= 0 && newY < map.GetLength(0) && map[newY, newX] != '#')
                                                        {
                                                            agentX = newX;
                                                            agentY = newY;
                                                        }

                                                    }
                                                           // return (agentX, agentY);

    }
}








// Game with simple  search algoritm

// using System;
// using System.IO;
// using System.Threading;

// namespace MyConsoleApp
// {
//     internal class Program
//     {
//                 static void Main(string[] args)
//                 {
//                     char[,] map = ReadMap(@"C:\Program Files\dotnet\MyConsoleApp\bin\Debug\net8.0\map.txt");
//                     ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
//                     Console.CursorVisible = false;
//                     Task.Run(() => {
//                       while(true){
//                     pressedKey = Console.ReadKey();
//                       }
//                     });
//                     int pacmanX = 1;
//                     int pacmanY = 1;
//                                   // int previousPacmanX = pacmanX;
//                                   // int previousPacmanY = pacmanY;
//                     int score = 0;

//                     int enemyX = 7;
//                     int enemyY = 3;
//                     int maxX = 5;
//                     bool isMovingRight = true;
                    
//                                 int agentX = 35;
//                                 int agentY = 4;


//           while (true)
//         {
//             Console.Clear();
//                 HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);
//             Console.ForegroundColor = ConsoleColor.Blue;
//             DrawMap(map);

            
//             Console.ForegroundColor = ConsoleColor.Yellow;
//             Console.SetCursorPosition(pacmanX, pacmanY);
//             Console.Write("@");

//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.SetCursorPosition(0, map.GetLength(0)); 
//             Console.Write($"Score: {score}");

            
//             Console.ForegroundColor = ConsoleColor.Green;
//             Console.SetCursorPosition(0, map.GetLength(0) + 1); 
//             Console.Write($"Last Key: {pressedKey.Key}");

            
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.SetCursorPosition(enemyX, enemyY);
//             Console.Write("P");
//             if(isMovingRight)
//             {
//               enemyX++;
//               if(enemyX >= maxX)
//               {
//                 isMovingRight = false;
//               }
//             } else {enemyX--;
//             if(enemyX <= 0)
//             {
//               isMovingRight = true;
//             }
//             }

//               agentPath(pacmanX, pacmanY, ref agentX, ref agentY, map);
//                         Console.ForegroundColor = ConsoleColor.Red; 
//                         Console.SetCursorPosition(agentX, agentY);
//                         Console.Write("A");

//                         if(pacmanX == agentX && pacmanY == agentY)
//                         {      

//                             Console.Clear();
//                             Console.SetCursorPosition(15,0);
//                             Console.WriteLine("yOu BeEn KeChEd By EnEmY ");
//                             break;
//                         }
//             Thread.Sleep(200);
//             if (Math.Abs(enemyX - pacmanX) <= 1 && Math.Abs(enemyY - pacmanY) <= 1)
//             {
//                 Console.Clear();
//                 Console.SetCursorPosition(15,0);
//                 Console.WriteLine("yOu BeEn KeChEd By EnEmY ");
//                 break;
//             }           
//               }
//     }
//         private static char[,] ReadMap(string path)
//         {
//             string[] file = File.ReadAllLines(path);

//             char[,] map = new char[file.Length, GetMaxLength(file)];

//             for (int y = 0; y < file.Length; y++)
//                 for (int x = 0; x < file[y].Length; x++)
//                     map[y, x] = file[y][x];
//             return map;
//         }
//         private static void DrawMap(char[,] map)
//         {
//             for (int y = 0; y < map.GetLength(0); y++)
//             {
//                 for (int x = 0; x < map.GetLength(1); x++)
//                 {
//                     Console.Write(map[y, x]);
//                 }
//                 Console.Write("\n");
//             }
//         }
//         private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
//         {
//             int[] direction = GetDirection(pressedKey);

//             int nextPacmanPositionX = pacmanX + direction[0];
//             int nextPacmanPositionY = pacmanY + direction[1];

//             char nextCell = map[nextPacmanPositionY, nextPacmanPositionX];

//             if (nextCell == ' ' || nextCell == '.')
//             {
//                 pacmanX = nextPacmanPositionX;
//                 pacmanY = nextPacmanPositionY;

//                 if (nextCell == '.')
//                 {
//                     score++;
//                     map[nextPacmanPositionY, nextPacmanPositionX] = ' ';
//                 }
//             }
//         }
//         private static int[] GetDirection(ConsoleKeyInfo pressedKey)
//         {
//             int[] direction = { 0, 0 };

//             if (pressedKey.Key == ConsoleKey.UpArrow)
//                 direction[1] = -1;
//             else if (pressedKey.Key == ConsoleKey.DownArrow)
//                 direction[1] = 1;
//             else if (pressedKey.Key == ConsoleKey.LeftArrow)
//                 direction[0] = -1;
//             else if (pressedKey.Key == ConsoleKey.RightArrow)
//                 direction[0] = 1;

//             return direction;
//         }
//         private static int GetMaxLength(string[] lines)
//         {
//             int maxLength = lines[0].Length;
//             foreach (var line in lines)
//                 if (line.Length > maxLength)
//                     maxLength = line.Length;
//             return maxLength;
//         }
//                                                     private static void agentPath(int pacmanX, int pacmanY, ref int agentX, ref int agentY, char[,] map)
//                                                     {
//                                                         int newX = agentX;
//                                                         int newY = agentY;

//                                                         if (pacmanX > agentX && (agentX + 1) < map.GetLength(1) && map[agentY, agentX + 1] != '#')
//                                                         {
//                                                             newX = agentX + 1;
//                                                         }
//                                                         else if (pacmanX < agentX && (agentX - 1) >= 0 && map[agentY, agentX - 1] != '#')
//                                                         {
//                                                             newX = agentX - 1;
//                                                         }
                                                        
//                                                         if (pacmanY > agentY && (agentY + 1) < map.GetLength(0) && map[agentY + 1, agentX] != '#')
//                                                         {
//                                                             newY = agentY + 1;
//                                                         }
//                                                         else if (pacmanY < agentY && (agentY - 1) >= 0 && map[agentY - 1, agentX] != '#')
//                                                         {
//                                                             newY = agentY - 1;
//                                                         }
//                                                         if (newX >= 0 && newX < map.GetLength(1) && newY >= 0 && newY < map.GetLength(0) && map[newY, newX] != '#')
//                                                         {
//                                                             agentX = newX;
//                                                             agentY = newY;
//                                                         }

//                                                     }
//                                                            // return (agentX, agentY);

//     }
// }










// https://www.youtube.com/watch?v=Y1KspRxortQ
//Buble sort algoritm !   __46- 50 minutes__;

// using System;
// using System.IO;
// namespace MyConsoleApp
// {
//     internal class Program
//   {

//     public class BubbleSorting
//     {
//       public static int[] DescendingSort(int[] array)
//       {
//           Console.WriteLine("Bubble sort");
//           int temp;
//           for (int i = 0; i < array.Length - 1; i++)
//           { 
//             for (int j = 0; j < array.Length - i - 1; j++)
//             {
//               if(array[j+1]>array[j])
//               {
//                 temp = array[j+1];
//                 array[j+1] = array[j];
//                 array[j]=temp;
//               }
//             }
//           }
//           return array;
//       }



//     }
//     static void Main(string[] args) 
//     {
//         var sortedArray = BubbleSorting.DescendingSort(CreateArray());
//         PrintArray(sortedArray);
//     }


//     private static int[] CreateArray()
//       {
//         return new int[10] {100,1,4,121,100,3,400,45,56,6};
//       }

//       private static void PrintArray(int[] array)
//       {
//         foreach(var item in array)
//         {
//           Console.WriteLine(item.ToString());
//         }
//       }

//   }

// }  



///////////////////////////////////////////////////////////////////////









//num2
//  7.20 min   https://www.youtube.com/watch?v=w8rRhAup4kg






//Computer club

// using System;
// using System.IO;
// namespace MyConsoleApp
// {
//     internal class Program
//   {
//     static void Main(string[] args) 
//     {
//       ComputerClub computerClub = new ComputerClub(8);
//       computerClub.Work();

//     }    


//   }
//   class ComputerClub
//   {

//     private int _money = 0;
//     private List<Computer> _computers = new List<Computer>();
//     private Queue<Client> _clients = new Queue<Client>();

//     public ComputerClub(int computersCount)
//     {
//       Random random = new Random();
//       for (int i = 0; i < computersCount; i++)
//       {
//         _computers.Add(new Computer(random.Next(5,15)));
//       }

//       CreateNewClients(25,random);
//     }

//     public void CreateNewClients(int count, Random random)
//     {

//       for (int i = 0; i < count; i++)
//       {
//         _clients.Enqueue(new Client(random.Next(100,251),random));
//       }
//     }

//   public void Work()
//   {
//     while (_clients.Count > 0)
//     {
//       Client newClient = _clients.Dequeue();
//       Console.WriteLine($"Balanse {_money}.Waiting for  new clients..");
//       Console.WriteLine($"New Client!.He want buy {newClient.DesiredMinutes} minutes.");
//       ShowAll();

//       Console.Write("\n You give him computer:  ");
//       string userInput = Console.ReadLine();
//       if(int.TryParse(userInput,out int computerNum))
//       {
//         computerNum -=1;
//         if(computerNum >= 0 && computerNum < _computers.Count)
//         {
//           if(_computers[computerNum].IsTaken)
//           {
//             Console.WriteLine("You try put client for not empty seat /Client became angry an go away! =( ");
//           }
//           else
//           {
//               if(newClient.CheckSolvency(_computers[computerNum]))
//               {
//                 Console.WriteLine("Client count money and completed payment " + (computerNum + 1));
//                 _money += newClient.Pay();
//                 _computers[computerNum].BecomeTaken(newClient);
//               }
//               else
//               {
//                 Console.WriteLine("Client have not enought money and go away.");
//               }
//           }
//         }
//         else
//         {Console.WriteLine("You dont now were put client .HClient is angry and he go away ! =( ");}
//       }
//       else
//       {
//         CreateNewClients(1,new Random());
//         Console.WriteLine("Not good input/Reapyt.");
//       }
//       Console.WriteLine("Put any key for next ckient.");
//       Console.ReadKey();
//       Console.Clear();
//       SpendOneMinute();
//     }
//   }
//   public void ShowAll()
//   {
//     Console.WriteLine("List of computers: ");
//     for (int i = 0; i < _computers.Count; i++)
//     {
//       Console.Write(i + 1 +"-");
//       _computers[i].ShowState();
//     }
//   }
//   private void SpendOneMinute()
//   {
//       foreach(var computer in _computers)
//       {
//         computer.SpendOneMinute();
//       }
//   }
//   }

//   class Computer
//   {

//     private Client _client;
//     private int _minutesRemaining;
//     public bool IsTaken
//     {
//         get
//         {
//           return _minutesRemaining > 0;
//         }
//     }
//     public int PricePerMinute{get;private set;}
//     public Computer(int pricePerMinute)
//     {
//       PricePerMinute = pricePerMinute;
//     }

//     public void BecomeTaken(Client client)
//     {
//       _client = client;
//       _minutesRemaining = _client.DesiredMinutes;
//     }
//     public void BecomeEmpty()
//     { 
//       _client = null;
//     }
//     public void SpendOneMinute()
//     {
//       _minutesRemaining--;
//     }
//     public void ShowState()
//     {
//       if(IsTaken)
//         Console.WriteLine($"Computer Busy: {_minutesRemaining}");
//       else
//         Console.WriteLine($"Compyter empty price per minute : {PricePerMinute}"); 
//     }
//   }


//   class Client
//   {

//     private int _money;
//     private int _moneyToPay;

//     public int DesiredMinutes{get;private set;}
//     public Client(int money,Random random)
//     {
//       _money = money;
//       DesiredMinutes = random.Next(30,90);
//     }

//     public bool CheckSolvency(Computer computer)
//   {
//     _moneyToPay = DesiredMinutes * computer.PricePerMinute;
//     if(_money >= _moneyToPay)
//     {
//       return true;
//     }
//     else
//     {
//       _moneyToPay = 0; 
//       return false;
//     }
//   }
//     public int Pay()
//     {
//       _money -= _moneyToPay;
//       return _moneyToPay;
//     }



//   }


// }  


















/// Console Mortal Kombat///


// using System;
// using System.IO;
// namespace MyConsoleApp
// {
//     internal class Program
//   {

//     static void Main(string[] args) 
//       {
//         bool isOpen = false ;

//         Fighter[] fighters = 
//         {
//           new Fighter("Scorpion",   350, 55, 10),
            // new Fighter("Liu Kang",   300, 45, 8),
            // new Fighter("Sub-Zero",   320, 60, 12),
            // new Fighter("Reptile",    280, 65, 7),
            // new Fighter("Kitana",     250, 50, 5),
            // new Fighter("Sonya Blade", 270, 40, 10),
            // new Fighter("Johnny Cage", 260, 50, 5),
            // new Fighter("Raiden",      350, 70, 15),
            // new Fighter("Goro",        500, 80, 20),
            // new Fighter("Shao Kahn",   600, 90, 25)
//         };
//         int fightNum;
//         int fightSecondNum;
        

//         Console.WriteLine("Welcome to Mortal Kombat")
//         for (int i = 0; i < fighters.Length; i++)
//         {
//           Console.Write(i + 1 + " ");
//           fighters[i].ShowStats();
//         }

//         Console.WriteLine("\n**"+ new string('-',25) + " **");

//         Console.Write("\n Choose num of your fighter:");
//         fightNum = Convert.ToInt32(Console.ReadLine()) - 1;
//         Fighter firstFighter = fighters[fightNum];


//         Console.Write("\n Choose num of your second_fighter:");
//         fightSecondNum = Convert.ToInt32(Console.ReadLine()) - 1;
//         Fighter secondFighter = null;
//         if( fightSecondNum == fightNum){isOpen = true ;}
//         while(isOpen == true)
//         {
//           if( fightSecondNum == fightNum)
//         {
//           Console.WriteLine("This fighter been chosed,try again!");
//           fightSecondNum = Convert.ToInt32(Console.ReadLine()) - 1;
//         } 
//         if( fightSecondNum != fightNum)
//           {
//             secondFighter = fighters[fightSecondNum];
//             isOpen = false;
//           }
//          Console.WriteLine("\n**"+ new string('-',25) + " **");

//         while(firstFighter.Health > 0 && secondFighter.Health > 0)
//         {
//           firstFighter.TakeDamage(secondFighter.Damage);
//           secondFighter.TakeDamage(firstFighter.Damage);

//           firstFighter.ShowCurrentHealth();
//           secondFighter.ShowCurrentHealth();
//         }
//         if(firstFighter.Health > 0 && secondFighter.Health <= 0)
//         {
//           Console.WriteLine("First fighter win!");
//           firstFighter.ShowCurrentHealth();
//         }
//         else if (firstFighter.Health <= 0 && secondFighter.Health > 0)
//         {
//           Console.WriteLine("Second fighter win!");
//           secondFighter.ShowCurrentHealth();
//         }
//         else if (firstFighter.Health <= 0 && secondFighter.Health <= 0)
//         {
//           Console.WriteLine("No one win !");
//         }
//       }
      
//     }
//   }

//     class Fighter
//       {
//           private string _name;
//           private int _health;
//           private int _damage;
//           private int _armor;

//           public int Health { 
//             get 
//             {
//               return _health;
//             }
//           }
//           public int Damage { 
//             get 
//             {
//               return _damage;
//             }
//           }

//           public Fighter(string name,int health,int damage,int armor)
//           {
//             _name = name;
//             _health = health;
//             _damage = damage;
//             _armor = armor;
//           }
//           public void ShowStats()
//             {
//               Console.WriteLine($"Fighter- {_name},health: {_health},damage: {_damage},armor: {_armor} ;");
//             }
//           public void ShowCurrentHealth()
//             {
//               Console.WriteLine($"Fighter- {_name} , health: {_health} ;");
//             }
//             public void TakeDamage(int damage)
//             {
//               _health -= damage - _armor;
//             }
//           }
// }








////////////




// Constructor practise 

  //  Car ferrari = new Car("F40",471,30,317.0f);
  //     Car ford = new Car();
  //     ferrari.ShowTechnicalPasport();
      
  //     ford.ShowTechnicalPasport();
      
  //   }

  // }


// class Car
// {
//     public string Name;
//     public int HorsePower;
//     public int Age;
//     public float MaxSpeed;

//     public Car (string name, int horsePower, int age, float maxSpeed)
//     {

//         Name = name;
//         if(horsePower < 0 ){ HorsePower = 1; } else {HorsePower = horsePower;}        
//         Age = age;
//         MaxSpeed = maxSpeed;

//     }
//     public Car()
//     {
//       Name = "Ford";
//       HorsePower = 500;
//       Age = 1;
//       MaxSpeed = 300;
//     }

//     public void ShowTechnicalPasport()
//     {
//       Console.WriteLine($"Name: {Name} \n HorsePower: {HorsePower} \n Car age: {Age} \n Max speed {MaxSpeed}");
//     }










//num 1
//5:31:00;
//   https://www.youtube.com/watch?v=w8rRhAup4kg



// using System;
// using System.IO;
// using System.Threading;

// namespace MyConsoleApp
// {
//     internal class Program
//     {
//         static void Main(string[] args)
//         {
//             char[,] map = ReadMap(@"C:\Program Files\dotnet\MyConsoleApp\bin\Debug\net8.0\map.txt");


//             ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

//             Console.CursorVisible = false;

//             Task.Run(() => {
//               while(true){
//             pressedKey = Console.ReadKey();
//               }


//             });


//             int pacmanX = 1;
//             int pacmanY = 1;
//             int score = 0;

//             int enemyX = 7;
//             int enemyY = 3;
//             int maxX = 5;
//             bool isMovingRight = true;

//   while (true)
// {
//     Console.Clear();
      


//         HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

//     // Рисование карты
//     Console.ForegroundColor = ConsoleColor.Blue;
//     DrawMap(map);

//     // Рисование Пакмана
//     Console.ForegroundColor = ConsoleColor.Yellow;
//     Console.SetCursorPosition(pacmanX, pacmanY);
//     Console.Write("@");

    
    


//     // Отображение текущего счёта (ниже карты)
//     Console.ForegroundColor = ConsoleColor.Red;
//     Console.SetCursorPosition(0, map.GetLength(0)); // Рисуем счёт под картой
//     Console.Write($"Score: {score}");

//     // Отображение последней нажатой клавиши
//     Console.ForegroundColor = ConsoleColor.Green;
//     Console.SetCursorPosition(0, map.GetLength(0) + 1); // Клавиша на следующей строке
//     Console.Write($"Last Key: {pressedKey.Key}");

//     // Считывание нажатия клавиши
//     // pressedKey = Console.ReadKey();
//     Console.ForegroundColor = ConsoleColor.Red;
//     Console.SetCursorPosition(enemyX, enemyY);
//     Console.Write("P");
//     if(isMovingRight)
//     {
//       enemyX++;
//       if(enemyX >= maxX)
//       {
//         isMovingRight = false;
//       }
//     } else {enemyX--;
//     if(enemyX <= 0)
//     {
//       isMovingRight = true;
//     }
//     }


//     Thread.Sleep(200);

//     if (Math.Abs(enemyX - pacmanX) <= 1 && Math.Abs(enemyY - pacmanY) <= 1)

//     {
        
//         Console.Clear();
//         Console.SetCursorPosition(15,0);
//         Console.WriteLine("yOu BeEn KeChEd By EnEmY ");
//         break;
//     }
  
// }

//         }

//         private static char[,] ReadMap(string path)
//         {
//             string[] file = File.ReadAllLines(path);

//             char[,] map = new char[file.Length, GetMaxLength(file)];

//             for (int y = 0; y < file.Length; y++)
//                 for (int x = 0; x < file[y].Length; x++)
//                     map[y, x] = file[y][x];

//             return map;
//         }

//         private static void DrawMap(char[,] map)
//         {
//             for (int y = 0; y < map.GetLength(0); y++)
//             {
//                 for (int x = 0; x < map.GetLength(1); x++)
//                 {
//                     Console.Write(map[y, x]);
//                 }
//                 Console.Write("\n");
//             }
//         }

//         private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
//         {
//             int[] direction = GetDirection(pressedKey);

//             int nextPacmanPositionX = pacmanX + direction[0];
//             int nextPacmanPositionY = pacmanY + direction[1];

//             char nextCell = map[nextPacmanPositionY, nextPacmanPositionX];

//             if (nextCell == ' ' || nextCell == '.')
//             {
//                 pacmanX = nextPacmanPositionX;
//                 pacmanY = nextPacmanPositionY;

//                 if (nextCell == '.')
//                 {
//                     score++;
//                     map[nextPacmanPositionY, nextPacmanPositionX] = ' ';
//                 }
//             }
//         }

//         private static int[] GetDirection(ConsoleKeyInfo pressedKey)
//         {
//             int[] direction = { 0, 0 };

//             if (pressedKey.Key == ConsoleKey.UpArrow)
//                 direction[1] = -1;
//             else if (pressedKey.Key == ConsoleKey.DownArrow)
//                 direction[1] = 1;
//             else if (pressedKey.Key == ConsoleKey.LeftArrow)
//                 direction[0] = -1;
//             else if (pressedKey.Key == ConsoleKey.RightArrow)
//                 direction[0] = 1;

//             return direction;
//         }

//         private static int GetMaxLength(string[] lines)
//         {
//             int maxLength = lines[0].Length;
//             foreach (var line in lines)
//                 if (line.Length > maxLength)
//                     maxLength = line.Length;
//             return maxLength;
//         }
//     }
// }




//Health bar 
    
//             int health = 5 ;
//             int maxHealth = 10;
//             int mana = 7 ;
//             int maxMana = 10;

// while(true)

// {

//   DrawBar(health , maxHealth, ConsoleColor.Green, 0,'|');
//             DrawBar(mana , maxMana, ConsoleColor.Blue, 1);

//             Console.SetCursorPosition(0,5);
//             Console.Write("Put a number of health: ");
//             health += Convert.ToInt32(Console.ReadLine());
//             Console.Write("Put a number of mana: ")
//             mana += Convert.ToInt32(Console.ReadLine())

//             Console.ReadKey();
//             Console.Clear();
// }

          

//         }
//         static void DrawBar(int value, int maxValue, ConsoleColor color, int position,char symbol = '_')
//         {
//             ConsoleColor defaultColor = Console.BackgroundColor;

//               string bar = "";

//               for (int i = 0; i < value; i++)
//               {
//                 bar += "symbol";
//               }
//               Console.SetCursorPosition(0,position);
//               Console.Write('[');
//               Console.BackgroundColor = color;
//               Console.Write(bar);
//               Console.BackgroundColor = defaultColor;

//               bar = "";
//               for(int i = value; i < maxValue; i++)
//               {
//                   bar += " ";
//               }

//               Console.Write(bar + ']');





//Перегрузка массива . 
    // int[] array1 = new int [5];
    //       int[,] array2 = new int[5,5];
    //       array1 = Resize(array1, 6);
    //       array2 = Resize(array2, 10,10);
    //       Console.WriteLine(array1.Length);

    //     }
    //     static int[] Resize (int[] array, int size)
    //     {
    //       int[] tempArray = new int[size];
    //       for (int i = 0; i < array.Length; i ++)
    //     {
    //       tempArray[i] = array[i];
    //     }
    //         array = tempArray;
    //         return array;
    //     }
    //     static int [,] Resize (int[] array , int x , int y)
    //     {
    //         int[,] tempArray = new int [x,y];

    //         for (int i = 0;i < array.GetLength(0); i++) {

    //           for (int j = 0;j < array.GetLength(1); j++)
            
    //         {
    //           tempArray[i,j] = array[i,j];
    //         }

    //           array = tempArray;
    //           return array;
    //         }


////////
//Travel game with collecting items for bag 
  //  Console.CursorVisible = false;
//             char[,] map = 
//             {
//                 {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ','X',' ',' ',' ',' ',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ','X',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ','#',' ',' ','X',' ','#','*',' ','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ','X','X',' ','#',' ',' ','X',' ','#','#',' ','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ','#',' ',' ','X',' ',' ',' ',' ',' ',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#','#','#'},
//                 {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#',' ',' ','#','#','#',' ',' ',' ','X',' ','*','X',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#','#',' ','#','*','#',' ',' ',' ','#',' ','X',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ','X',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
//                 {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
//             };

//             int userX = 6, userY = 6;
//             char[] bag = new char[1];
            
//             while (true)
//             {
//                 Console.SetCursorPosition(0, map.GetLength(0) + 1);

//                 Console.Write("Bag: ");
//                 for (int i = 0; i < bag.Length; i++)
//                 {
//                     Console.Write(bag[i] + " ");
                    
//                 }
//                 Console.SetCursorPosition(0, 0);
//                 for (int i = 0; i < map.GetLength(0); i++)
//                 {
//                     for (int j = 0; j < map.GetLength(1); j++)
//                     {
//                         Console.Write(map[i, j]);
//                     }
//                     Console.WriteLine();
//                 }
//                 Console.SetCursorPosition(userY, userX);
//                 Console.Write('@');
//                 ConsoleKeyInfo charKey = Console.ReadKey();
//                 switch (charKey.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         if (map[userX - 1, userY] != '#')
//                         {
//                             userX--;
//                         }
//                         break;
//                     case ConsoleKey.DownArrow:
//                         if (map[userX + 1, userY] != '#')
//                         {
//                             userX++;
//                         }
//                         break;
//                     case ConsoleKey.LeftArrow:
//                         if (map[userX, userY - 1] != '#')
//                         {
//                             userY--;
//                         }
//                         break;
//                     case ConsoleKey.RightArrow:
//                         if (map[userX, userY + 1] != '#')
//                         {
//                             userY++;
//                         }
//                         break;
//                 }

//                 if (map[userX, userY] == '*')
//                 {
//                     map[userX, userY] = 'o';
//                     char[] tempBag = new char[bag.Length + 1];
//                     for (int i = 0; i < bag.Length; i++)
//                     {
//                         tempBag[i] = bag[i];
//                     }
//                     tempBag[tempBag.Length - 1] = '*';
//                     bag = tempBag;

//                 Console.Clear();
//                                     if (bag.Length-1 == 5)
// {
//     Console.SetCursorPosition(0, map.GetLength(0) + 2);
//     Console.WriteLine("Complete!");
//     Console.ReadKey(); 
//     break; 
// }

//                 }
//                 if (map[userX, userY] == 'X'){
//                   Console.SetCursorPosition(0, map.GetLength(0) + 2);
//     Console.WriteLine("You fail next time dont go on X");
//     Console.ReadKey(); 
//     break; 
//             }
///////////////////////////////////////////////////////////////////////////////////////////
//travel game min max algoritm and animate traps


      //  Console.CursorVisible = false;
      //       char[,] map =
      //       {
      //           {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ','-',' ',' ',' ',' ','#',' ',' ',' ',' ','#','*',' ','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ','X','X',' ','#',' ',' ',' ',' ','#','#',' ','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ','#',' ',' ','X',' ',' ',' ',' ',' ',' ',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','-',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#','#','#'},
      //           {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#',' ',' ','#','#','#',' ',' ',' ',' ',' ','*','X',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#','#',' ','#','*','#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ','#','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','-',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ','X',' ','X',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
      //           {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
      //       };

      //       int userX = 12, userY = 12;
      //       int enemyX = 1, enemyY = 1; // Стартовая позиция противника
      //       char[] bag = new char[1];

      //       // Автоматический поиск всех X и - на карте
      //       List<(int x, int y, int direction, char type)> movingTraps = new List<(int, int, int, char)>();
      //       for (int i = 0; i < map.GetLength(0); i++)
      //       {
      //           for (int j = 0; j < map.GetLength(1); j++)
      //           {
      //               if (map[i, j] == 'X')
      //               {
      //                   movingTraps.Add((i, j, 1, 'X')); // X движется вниз сначала
      //               }
      //               else if (map[i, j] == '-')
      //               {
      //                   movingTraps.Add((i, j, 1, '-')); // - движется вправо сначала
      //               }
      //           }
      //       }

      //       while (true)
      //       {
      //           Console.SetCursorPosition(0, map.GetLength(0) + 1);

      //           Console.Write("Bag: ");
      //           for (int i = 0; i < bag.Length; i++)
      //           {
      //               Console.Write(bag[i] + " ");
      //           }

      //           Console.SetCursorPosition(0, 0);
      //           for (int i = 0; i < map.GetLength(0); i++)
      //           {
      //               for (int j = 0; j < map.GetLength(1); j++)
      //               {
      //                   Console.Write(map[i, j]);
      //               }
      //               Console.WriteLine();
      //           }

      //           Console.SetCursorPosition(userY, userX);
      //           Console.Write('@');
      //           Console.SetCursorPosition(enemyY, enemyX);
      //           Console.Write('E');

      //           // Движение ловушек
      //           for (int i = 0; i < movingTraps.Count; i++)
      //           {
      //               int x = movingTraps[i].x;
      //               int y = movingTraps[i].y;
      //               int direction = movingTraps[i].direction;
      //               char type = movingTraps[i].type;

      //               // Убираем объект с текущей позиции
      //               map[x, y] = ' ';

      //               // Обновляем позицию в зависимости от типа объекта
      //               if (type == 'X') // Движение вверх-вниз
      //               {
      //                   x += direction;
      //                   if (direction == 1) // Вниз
      //                   {
      //                       direction = -1; // Меняем направление на вверх
      //                   }
      //                   else if (direction == -1) // Вверх
      //                   {
      //                       direction = 1; // Меняем направление на вниз
      //                   }
      //               }
      //               else if (type == '-') // Движение влево-вправо
      //               {
      //                   y += direction;
      //                   if (direction == 1) // Вправо
      //                   {
      //                       direction = -1; // Меняем направление на влево
      //                   }
      //                   else if (direction == -1) // Влево
      //                   {
      //                       direction = 1; // Меняем направление на вправо
      //                   }
      //               }

      //               // Обновляем объект на новой позиции
      //               map[x, y] = type;
      //               movingTraps[i] = (x, y, direction, type); // Сохраняем новую позицию и направление

      //               // Проверяем столкновение с игроком
      //               if (x == userX && y == userY)
      //               {
      //                   Console.SetCursorPosition(0, map.GetLength(0) + 2);
      //                   Console.WriteLine("You were caught by a trap!");
      //                   Console.ReadKey();
      //                   return;
      //               }
      //           }

      //           // Логика движения игрока
      //           ConsoleKeyInfo charKey = Console.ReadKey();
      //           switch (charKey.Key)
      //           {
      //               case ConsoleKey.UpArrow:
      //                   if (map[userX - 1, userY] != '#')
      //                   {
      //                       userX--;
      //                   }
      //                   break;
      //               case ConsoleKey.DownArrow:
      //                   if (map[userX + 1, userY] != '#')
      //                   {
      //                       userX++;
      //                   }
      //                   break;
      //               case ConsoleKey.LeftArrow:
      //                   if (map[userX, userY - 1] != '#')
      //                   {
      //                       userY--;
      //                   }
      //                   break;
      //               case ConsoleKey.RightArrow:
      //                   if (map[userX, userY + 1] != '#')
      //                   {
      //                       userY++;
      //                   }
      //                   break;
      //           }

      //           // Проверка, если игрок собирает '*'
      //           if (map[userX, userY] == '*')
      //           {
      //               map[userX, userY] = 'o';
      //               char[] tempBag = new char[bag.Length + 1];
      //               for (int i = 0; i < bag.Length; i++)
      //               {
      //                   tempBag[i] = bag[i];
      //               }
      //               tempBag[tempBag.Length - 1] = '*';
      //               bag = tempBag;

      //               Console.Clear();
      //               if (bag.Length - 1 == 5)
      //               {
      //                   Console.SetCursorPosition(0, map.GetLength(0) + 2);
      //                   Console.WriteLine("Complete!");
      //                   Console.ReadKey();
      //                   break;
      //               }
      //           }

      //           // Логика движения противника
      //           (int newEnemyX, int newEnemyY) = GetEnemyMove(enemyX, enemyY, userX, userY, map);
      //           enemyX = newEnemyX;
      //           enemyY = newEnemyY;

      //           if (enemyX == userX && enemyY == userY)
      //           {
      //               Console.SetCursorPosition(0, map.GetLength(0) + 2);
      //               Console.WriteLine("You were caught!");
      //               Console.ReadKey();
      //               break;
      //                Console.Clear();
      //           }
      //       }
      //   }

      //   static (int, int) GetEnemyMove(int enemyX, int enemyY, int userX, int userY, char[,] map)
      //   {
      //       // Возможные направления движения
      //       (int dx, int dy)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };
      //       (int, int) bestMove = (enemyX, enemyY);
      //       int bestDistance = int.MaxValue;

      //       foreach (var (dx, dy) in directions)
      //       {
      //           int newX = enemyX + dx;
      //           int newY = enemyY + dy;

      //           if (map[newX, newY] == '#' || map[newX, newY] == '*') continue; // Пропускаем стены и ловушки

      //           int distance = Math.Abs(newX - userX) + Math.Abs(newY - userY); // Манхэттенское расстояние
      //           if (distance < bestDistance)
      //           {
      //               bestDistance = distance;
      //               bestMove = (newX, newY);
      //           }
      //       }

      //       return bestMove;
      //   }





/////////////////////////////////////







//////

///Gladiators fight
///
//  Random rand = new Random();

//          float health1 = rand.Next(40, 60);
// 				 int damage1 = rand.Next(10, 20);
// 				 int armor1 = rand.Next(1,3);

// 				 float health2 = rand.Next(30,45);
// 				 int damage2 = rand.Next(20,35);
// 				 int armor2 = rand.Next(1,2);

//           Console.WriteLine ($"Gladiator 1 - {health1} health, {damage1} attak, {armor1} armor");
// 				  Console.WriteLine ($"Gladiator 2 - {health2} health, {damage2} attak, {armor2} armor");



// 					while(health1 > 0 && health2 >0)
// 					{
//               health1 -= Convert.ToSingle(rand.Next(0, damage2 + 1)) / 100 * armor1;
// 							health2 -= Convert.ToSingle(rand.Next(0, damage1 + 1)) / 100 * armor2;


//             Console.WriteLine("Health gladiator 1 : " + health1);
// 						 Console.WriteLine("Health gladiator 2 : " + health2);


// 					} if ( health1 <= 0 && health2 <= 0){Console.WriteLine("No one win");
					
					
// 					  } else if (health1 <=0){Console.WriteLine("Gladiator 1 day");} else if (health2 <= 0 ){Console.WriteLine("Gladiator 2 day"); }

////////

//Library
  //  {
  //       bool isOpen = true;
  //       string[,] books =
	// 			{
  //         {"Alexsandr","Mixail","Sergey"},
  //         {"Vlad","Ivan","Bogdan"},
	// 				{"Nad","Jekab","Patrik"}
	// 			};

  //         while (isOpen)
	// 				{
  //           Console.SetCursorPosition(0,20);
	// 					Console.WriteLine("\n All list of avtors: \n");
	// 					for (int i = 0; i < books.GetLength(0); i++)
	// 					{
  //               for(int j = 0; j < books.GetLength(1); j++)
	// 							{
  //                 Console.Write(books[i,j] + "|" );
	// 							}
  //                 Console.WriteLine();
	// 					}
	// 					 Console.SetCursorPosition(0,20);
	// 					 Console.WriteLine("Library");
	// 					 Console.WriteLine("\n1 - name of avtor from index of book . \n\n2 -  find book from name of avtor. \n\n3 - exit.");
	// 					 Console.Write("Put punkt of menu: ");
	// 					 switch(Convert.ToInt32(Console.ReadLine()))
	// 					 {
  //             case  1:
	// 						int line, column;
	// 						Console.Write("Put number of table:");
	// 						line = Convert.ToInt32(Console.ReadLine()) -1;
  //             Console.Write("Put number of column:");
	// 						column = Convert.ToInt32(Console.ReadLine()) -1;
	// 						Console.WriteLine("This avtor" + books[line,column]);
	// 						break;
	// 						case  2:
	// 						string author;
	// 						bool authorIsFound = false ;
	// 						Console.Write("Put avtors name");
	// 						author = Console.ReadLine();
	// 						for(int i = 0;i < books.GetLength(0);i++){

							
	// 						for (int g = 0; g < books.GetLength(1); g++)
	// 						{

  //                   if(author.ToLower() == books[i,g].ToLower())
	// 									{
  //                     Console.Write($"Author {books[i,g]} on the table  {i + 1} and place {g + 1}");
  //                     authorIsFound = true;
	// 									}
							  
	// 						}
	// 						}
	// 						if(authorIsFound = false)
	// 						{

  //               Console.WriteLine("Propably name of the author dosent correct");
	// 						}
	// 						break;
	// 						case  3:
	// 						default:
	// 						Console.WriteLine("Wrong command");
	// 						break;
  //         }

	// 				if(isOpen){

  //                     Console.WriteLine("\nClick any key for continue.");

	// 				}
  //             Console.ReadKey();
	// 						Console.Clear();
	// 				}


//Airporte seats

  //  int[] sectors = { 6, 28, 15, 15, 17 };
  //           bool isOpen = true;

  //           while (isOpen)
  //           {
  //               Console.SetCursorPosition(0, 18);
  //               for (int i = 0; i < sectors.Length; i++)
  //               {
  //                   Console.WriteLine($"В секторе {i + 1} свободно {sectors[i]} мест.");
  //               }

  //               Console.SetCursorPosition(0, 0);
  //               Console.WriteLine("Sign in for Receipt");
  //               Console.WriteLine("\n\n1 - Apply for a place\n\n2 - Exit from the program.\n\n");
  //               Console.Write("Enter the number of the command: ");

  //               switch (Convert.ToInt32(Console.ReadLine()))
  //               {
  //                   case 1:
  //                       int userSector, userPlaceAmount;
  //                       Console.Write("What sector do you want to choose? ");
  //                       userSector = Convert.ToInt32(Console.ReadLine()) - 1;

  //                       if (userSector < 0 || userSector >= sectors.Length)
  //                       {
  //                           Console.WriteLine("Invalid sector");
  //                           break;
  //                       }

  //                       Console.Write("How many places do you want to take? ");
  //                       userPlaceAmount = Convert.ToInt32(Console.ReadLine());

  //                       if (userPlaceAmount < 0 || sectors[userSector] < userPlaceAmount)
  //                       {
  //                           Console.WriteLine($"Not enough seats in sector {userSector + 1}. We have {sectors[userSector]}.");
  //                           break;
  //                       }

  //                       sectors[userSector] -= userPlaceAmount;
	// 											Console.WriteLine("Sucsess!");
  //                       break;

  //                   case 2:
  //                       isOpen = false;
  //                       break;

  //                   default:
  //                       Console.WriteLine("Invalid command.");
  //                       break;
  //               }

  //               Console.ReadKey();
  //               Console.Clear();
  //           }


///Guess the number


//         int number;
// 						int lower, higher;

// 						int triesCount = 5;
// 						int userInput;
// 						Random rand = new Random();


// 						number = rand.Next(0, 101);
// 						lower = rand.Next(number - 10,number);
// 						higher = rand.Next(number + 1,number + 10);

// 						Console.WriteLine($"Bolshe {lower} but smaller then {higher}.");


// 						Console.WriteLine($"What the number ? You have {triesCount} tries for answer.");


//            while(triesCount-- > 0)
// 					 {
// Console.Write("Your answer : ");
// userInput = Convert.ToInt32(Console.ReadLine());
// if(userInput == number)
// {
// Console.WriteLine("Right ! ");

// break;
// } else {Console.WriteLine("Try again"); }



// 					 } if(triesCount < 0){Console.WriteLine($"Next nime !The number is {number}");}


//USD AND RUB 
						//   float rublesInWallet;
            // float dollarsInWallet;

            // int rubToUsd = 64, usdToRub = 66;
            // float exchangeCurrencyCount;
            // string desiredOperation;

            // Console.WriteLine("Welcome!");
            // Console.Write("Enter your balance in rubles: ");
            // rublesInWallet = Convert.ToSingle(Console.ReadLine());

            // Console.Write("Enter your balance in dollars: ");
            // dollarsInWallet = Convert.ToSingle(Console.ReadLine());

            // Console.WriteLine("Choose an operation:");
            // Console.WriteLine("1 - Exchange rubles for dollars");
            // Console.WriteLine("2 - Exchange dollars for rubles");
            // Console.Write("Your choice: ");
            // desiredOperation = Console.ReadLine();

            // switch (desiredOperation)
            // {
            //     case "1":
            //         Console.WriteLine("You chose to exchange rubles for dollars.");
            //         Console.WriteLine("How much do you want to exchange?");
            //         exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

            //         if (rublesInWallet >= exchangeCurrencyCount)
            //         {
            //             rublesInWallet -= exchangeCurrencyCount;
            //             dollarsInWallet += exchangeCurrencyCount / rubToUsd;
            //             Console.WriteLine("Exchange successful!");
            //         }
            //         else
            //         {
            //             Console.WriteLine("Not enough rubles.");
            //         }
            //         break;

            //     case "2":
            //         Console.WriteLine("You chose to exchange dollars for rubles.");
            //         Console.WriteLine("How much do you want to exchange?");
            //         exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

            //         if (dollarsInWallet >= exchangeCurrencyCount)
            //         {
            //             dollarsInWallet -= exchangeCurrencyCount;
            //             rublesInWallet += exchangeCurrencyCount * usdToRub;
            //             Console.WriteLine("Exchange successful!");
            //         }
            //         else
            //         {
            //             Console.WriteLine("Not enough dollars.");
            //         }
            //         break;

            //     default:
            //         Console.WriteLine("Invalid operation chosen.");
            //         break;
            // }

            // Console.WriteLine($"Your balance: {rublesInWallet} rubles, {dollarsInWallet} dollars");



//"Happy Birthday!

						// 				int age;
						// 	Console.Write("Put your age: ");
						// 	age =  Console.ToInt32(Console.ReadLine));
						// 	while(age-- >0)
						// 	{
						
						// Console.WriteLine("Happy Birthday!");
						// if(age == 5)
						// {
						// continue;

						// }


						// 	}



						//    int age;
            // Console.Write("Put your age: ");
            // age = Convert.ToInt32(Console.ReadLine());

            // for (int i = 0; i <= age; i++) // исправлено условие цикла
            // {
            //     Console.WriteLine($"You have {i} age ");
            // }





          /// Fight with enemy

//   {
//         int playerHealth = 100;
// 				int playerDamage = 10;
// 				int enemyHealth = 50;
// 				int enemyDamage = 15;

//   while(playerHealth > 0 && enemyHealth > 0 )
// 	{
//      playerHealth -= enemyDamage;
// 		 enemyHealth -= playerDamage;


// 		 Console.WriteLine(playerHealth + "Player.");
// 		  Console.WriteLine(enemyHealth + "Enemy.");
// 	}

//       if(playerHealth <= 0 && enemyHealth <= 0)
// 			{

// Console.WriteLine("No one win");


// 			}else if(playerHealth <= 0)
// 			{
// 				Console.WriteLine("Enemy win");
// 				} else if(enemyHealth <= 0)
// 				{
// 					Console.WriteLine("Player win");
					
// 					} 




						//  int triesCount = 5;
            // string password = "123456";
            // string userInput;

            // for (int i = 0; i < triesCount; i++)
            // {
            //     Console.Write("Put your password: ");
            //     userInput = Console.ReadLine();

            //     if (userInput == password)
            //     {
            //         Console.WriteLine("Secrets.");
            //         break;
            //     }
            //     else
            //     {
            //         Console.WriteLine("Wrong password");
            //         Console.WriteLine("You have " + (triesCount - (i + 1)) + " attempts left");
            //     }
            // }


						//      float money;
            // int years;
            // int precent;

            // Console.Write("Put money: ");
            // money = Convert.ToSingle(Console.ReadLine());

            // Console.Write("How long? ");
            // years = Convert.ToInt32(Console.ReadLine());

            // Console.Write("Percent? ");
            // precent = Convert.ToInt32(Console.ReadLine());

            // for (int i = 0; i < years; i++)
            // {
            //     money += (money * precent) / 100;
            //     Console.WriteLine("In this year: " + money);
            // }

            // Console.ReadKey(); // Ожидание по завершению программы