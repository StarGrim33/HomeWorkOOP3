using System.Runtime.CompilerServices;
using System.Xml.Linq;

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
            const string CommandUnbanPlayer = "5";
            const string CommandExit = "6";

            bool isProgramOn = true;
            Database database = new Database();

            while (isProgramOn)
            {
                Console.Clear();
                Console.WriteLine($"Меню: \n{CommandAddPlayer}-Добавить игрока");
                Console.WriteLine($"{CommandShowAllPlayers}-Показать все досье");
                Console.WriteLine($"{CommandDeletePlayer}-Удалить игрока");
                Console.WriteLine($"{CommandBanPlayer}-Забанить игрока");
                Console.WriteLine($"{CommandUnbanPlayer}-Разбанить игрока");
                Console.WriteLine($"{CommandExit}-Выход");

                string userMenuNavigate = Console.ReadLine();

                switch (userMenuNavigate)
                {
                    case CommandAddPlayer:
                        database.AddPlayer();
                        break;

                    case CommandShowAllPlayers:
                        database.ShowAllPlayers();
                        break;

                    case CommandDeletePlayer:
                        database.DeletePlayer();
                        break;

                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandExit:
                        isProgramOn = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка ввода комманды");
                        break;
                }
            }
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private int _lastId = 0;

        public void AddPlayer()
        {
            int id = ++_lastId;

            Console.WriteLine("Введите имя игрока: ");
            string? name = Console.ReadLine();

            Console.WriteLine("Введите уровень игрока: ");

            if (int.TryParse(Console.ReadLine(), out int level))
            {
                if (level < 1)
                {
                    level = 1;
                    PrintUnsuccessfulText("У игрока не может быть нулевой или отрицательный уровень, присвоен 1 уровень");
                }
                else
                {
                    bool _isBanned = SetBannedStatus();
                    _players.Add(new Player(id, name, level, _isBanned));
                }
            }
        }

        public void ShowAllPlayers()
        {
            Console.Clear();
            Console.WriteLine("Список игроков: \n");

            if (_players.Count > 0)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.");
                    _players[i].ShowInfo();
                }
            }
            else
            {
                PrintUnsuccessfulText("База данных пуста");
            }

            Console.ReadKey();
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Удаление игрока");

            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
                PrintSuccessfulText("Игрок удален");
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
                PrintSuccessfulText("Игрок забанен");
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
                PrintSuccessfulText("Игрок разблокирован");
            }
        }

        private void PrintSuccessfulText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ResetColor();
        }

        private void PrintUnsuccessfulText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }

        private bool SetBannedStatus()
        {
            bool isBanned = false;
            string banned = "1";
            string notBanned = "2";

            Console.WriteLine("Введите статус игрока: ");
            Console.WriteLine($"{banned}-Заблокирован");
            Console.WriteLine($"{notBanned}-Не заблокирован");

            string userInput = Console.ReadLine();

            if (userInput == banned)
            {
                isBanned = true;
            }
            else if (userInput == notBanned)
            {
                isBanned = false;
            }
            else
            {
                PrintUnsuccessfulText("Ошибка ввода данных");
                Console.ReadKey();
            }

            return isBanned;
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;

            Console.WriteLine("Введите id пользователя");

            if (int.TryParse(Console.ReadLine(), out int number))
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].Id == number)
                    {
                        player = _players[i];
                        return true;
                    }
                    else
                    {
                        PrintUnsuccessfulText("Игрок не найден");
                    }
                }
            }
            else
            {
                PrintUnsuccessfulText("Ошибка ввода данных");
            }

            return false;
        }
    }

    class Player
    {
        public Player(int id, string name, int level, bool isBanned)
        {
            Id = id;
            NickName = name;
            Level = level;
            IsBanned = isBanned;
        }

        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }
        public int Id { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Id игрока: {Id} \nИмя игрока: {NickName} \nУровень игрока: {Level}");

            if (IsBanned)
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

        public void Unban()
        {
            IsBanned = false;
        }
    }
}