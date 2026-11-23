void Stage1(Player plr)
{
    plr.ShowStats();
    Console.WriteLine("You are on the crossway where should you go next?\n- 1. Right\n- 2. Left");
    string way = Console.ReadLine()!;
    switch (way)
    {
        case "1":
            {
                Console.WriteLine("You found a treasure ( + 20 gold )");
                plr.Gold += 20;
                break;
            }
        case "2":
            {
                Console.WriteLine("The arrow hitted your back! ( - 30 HP )");
                plr.Hp -= 30;
                break;
            }
        default:
            {
                Console.WriteLine("Wrong select!");
                Stage1(plr);
                break;
            }
    }
}

void Stage2(Player plr)
{
    try
    {
        plr.ShowStats();
        Console.WriteLine("Welcome to mystery market, {0}! What do you want to do?\n- 1. Buy stronger sword ( - 50 Gold )\n- 2. Buy elexir of young ( + 50 Hp, -30 Gold )\n- 3. Go away", plr.Name);
        string way = Console.ReadLine()!;
        switch (way)
        {
            case "1":
                {
                    plr.Gold -= 50;
                    Console.WriteLine("New sword seems useful ( - 50 Gold )");
                    plr.GiveSword();
                    break;
                }
            case "2":
                {
                    plr.Gold -= 30;
                    Console.WriteLine("The elexir restored your power ( + 50 Hp )");
                    plr.Hp += 50;
                    break;
                }
            case "3":
                {
                    Console.WriteLine("You have gone...");
                    break;
                }
            default:
                {
                    Console.WriteLine("Wrong select!");
                    Stage2(plr);
                    break;
                }
        }
    }
    catch (Exception e)
    {
        if (e.Message == "x") return;
        else throw;
    }
}

void Stage3(Player plr)
{
    plr.ShowStats();
    Console.WriteLine("You reached the dragon's cave! The battle begins!");
    if (plr.CanAttack())
    {
        Console.WriteLine("VICTORY! With your sword and high health, you defeated the dragon!");
    }
    else if (plr.Hp > 50)
    {
        Console.WriteLine("LUCKY WIN! You had no sword, but defeated the dragon with sheer agility!");
    }
    else
    {
        Console.WriteLine("The dragon was too strong for you...");
        plr.Hp -= 100;
    }
}

try
{
    Console.WriteLine("Welcome to SwedikWorld!");

    Player plr = new Player();

    Console.WriteLine("Hello {0}, you have {1} gold and {2} hp!", plr.Name, plr.Gold, plr.Hp);

    Stage1(plr);
    Stage2(plr);
    Stage3(plr);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

class Utils
{
    public static bool isEmptyStr(string? str)
    {
        return str == null || str.Trim() == "";
    }
}

class Player
{
    public Player()
    {
        Console.WriteLine("Please enter your name:");
        string? name = Console.ReadLine()?.Trim();
        Console.WriteLine("Are you rich (enter your gold count > 0, only integer):");
        string? gold_promt = Console.ReadLine()?.Trim();
        Console.WriteLine("Did you eat today (enter your hp from 0 to 100, only integer):");
        string? hp_promt = Console.ReadLine()?.Trim();

        if (Utils.isEmptyStr(name) || Utils.isEmptyStr(gold_promt) || Utils.isEmptyStr(hp_promt)) throw new Exception("Opps... do you skip some questions?");

        int.TryParse(gold_promt!, out int _gold);
        int.TryParse(hp_promt!, out int _hp);

        Name = name!;
        Gold = _gold;
        Hp = _hp;

    }
    public Player(string name, int gold, int hp)
    {
        Name = name;
        Gold = gold;
        Hp = hp;
    }

    private bool hasSword = false;
    private int gold;
    private int hp;
    public string Name;
    public int Gold
    {
        get => gold;
        set
        {
            if (value < 0)
            {
                Console.WriteLine("You are out of gold, you was beaten ( -40 Hp )");
                Hp -= 40;
                gold = 0;
                throw new Exception("x");
            }
            else gold = value;
        }
    }
    public int Hp
    {
        get => hp;
        set
        {
            if (value <= 0) throw new Exception("Game over!");
            if (value > 100) hp = 100;
            else hp = value;
        }
    }
    public void GiveSword()
    {
        this.hasSword = true;
    }
    public bool CanAttack()
    {
        return hasSword && Hp > 20;
    }
    public void ShowStats()
    {
        Console.WriteLine("{0}: Gold {1}, HP {2}, Inventory: {3}", Name, Gold, Hp, hasSword ? "Sword" : "None");
    }
};