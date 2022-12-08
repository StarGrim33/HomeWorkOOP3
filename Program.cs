namespace HomeWorkOOP3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandShowAllPlayers = "2";
            const string CommandDeletePlayer = "3";
            const string CommandBanPlayer = "4";
            const string CommandUnBanPlayer = "5";
            const string CommandExit = "6";
            
            DataBase list = new DataBase();
            bool isProgramOn = true;

            
            while (isProgramOn)
            {
                Console.Clear();
                Console.WriteLine($"Меню: \n{CommandAddPlayer}-Добавить игрока\n{CommandShowAllPlayers}-Показать все досье\n{CommandDeletePlayer}-Удалить игрока\n{CommandBanPlayer}-Забанить игрока\n" +
                    $"{CommandUnBanPlayer}-Разбанить игрока\n{CommandExit}-Выход");

                string userMenuNavigate = Console.ReadLine();

                switch (userMenuNavigate)
                {
                    case CommandAddPlayer:
                        list.AddPlayer();
                        break;
                    case CommandShowAllPlayers:
                        list.ShowAllPlayers();
                        break;
                    case CommandDeletePlayer:
                        list.DeletePlayer();
                        break;
                    case CommandBanPlayer:
                        list.BanPlayer();
                        break;
                    case CommandUnBanPlayer:
                        list.UnBanPlayer();
                        break;
                    case CommandExit:
                        isProgramOn = false;
                        break;
                }
            }
        }
    }
    class DataBase
    {
        public Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private int _idPlayer = 0;

        public void AddPlayer()
        {
            Console.WriteLine("Введите идентификатор игрока: ");
            bool isId = int.TryParse(Console.ReadLine(), out _idPlayer);

            Console.WriteLine("Введите имя игрока: ");
            string? name = Console.ReadLine();

            Console.WriteLine("Введите уровень игрока: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int level);

            if(isNumber == false || isId == false)
            {
                PrintUnsuccessfulText("\nНекорректный ввод.");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Игрок забанен ? Введите (Да или Нет)");
                string userInput = Console.ReadLine();
                bool isBanned;

                if(userInput == "Да")
                {
                    isBanned = true;
                    _players.Add(_idPlayer, new Player(name, level, isBanned));
                    PrintSuccessfulText("Игрок добавлен");
                    Console.ReadKey();
                }
                else if(userInput == "Нет")
                {
                    isBanned = false;
                    _players.Add(_idPlayer, new Player(name, level, isBanned));
                    PrintSuccessfulText("Игрок добавлен");
                    Console.ReadKey();
                }
                else
                {
                    PrintUnsuccessfulText("Некорректный ввод");
                }
            }
        }

        public void ShowAllPlayers()
        {
            Console.Clear();
            Console.WriteLine("Список игроков: \n");

            if(_players.Count >= 1)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    Console.WriteLine("Идентификатор: " + _idPlayer);
                    _players[i+1].ShowInfo();
                }
                Console.ReadKey();
            }
            else
            {
                PrintUnsuccessfulText("База данных пуста");
                Console.ReadKey();
            }
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Введите идентификатор игрока, которого хотите удалить: ");
            bool userInput = int.TryParse(Console.ReadLine(), out int userInputInt);
            
            if(userInput == true & _players.ContainsKey(userInputInt) == true)
            {
                _players.Remove(userInputInt);
                PrintSuccessfulText("\nИгрок удален");
            }
            else
            {
                PrintUnsuccessfulText("Некорректный ввод, введите номер идентификатора игрока");
                Console.ReadKey();
            }
        }

        public void BanPlayer()
        {
            Console.WriteLine("Введите идентификатор игрока: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int userInputInt);

            if(_players.ContainsKey(userInputInt))
            {
                if (_players[userInputInt].IsBanned == false)
                {
                    _players[userInputInt].Ban();
                    PrintSuccessfulText("Игрок забанен");
                }
                else
                {
                    PrintUnsuccessfulText("Игрок уже забанен");
                }
            }
            else
            {
                PrintUnsuccessfulText("Такого игрока нет");
            }
        }

        public void UnBanPlayer()
        {
            Console.WriteLine("Введите идентификатор игрока: ");
            bool isNumber = int.TryParse(Console.ReadLine(),out int userInputInt);

            if (_players.ContainsKey(userInputInt))
            {
                if (_players[userInputInt].IsBanned == true)
                {
                    _players[userInputInt].UnBan();
                    PrintSuccessfulText("Игрок разбанен");
                }
                else
                {
                    PrintUnsuccessfulText("У игрока нет бана");
                }
            }
            else
            {
                PrintUnsuccessfulText("Такого игрока нет");
            }
        }

        public void PrintSuccessfulText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ResetColor();
        }

        public void PrintUnsuccessfulText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }

        

    }

    class Player
    {
        public Player(string name, int level, bool isBanned)
        {
            NickName = name;
            Level = level;
            IsBanned = isBanned;
        }

        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя игрока: {NickName} \nУровень игрока: {Level}");

            if(IsBanned)
            {
                Console.WriteLine("Игрок забанен");
            }
            else
            {
                Console.WriteLine("Без бана");
            }
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void UnBan()
        {
            IsBanned = false;
        }
    }

}