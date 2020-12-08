using System;

namespace oop_9
{
    class Game
    {
        public delegate void GameHandler();
        public event GameHandler Attack;
        public event GameHandler Heal;
        
        public void CommandAttack()
        {
            if (Attack != null)
            {
                Console.WriteLine("Все существа в игре атакованы");
                Attack();
            }
        }

        public void CommandHeal()
        {
            if (Heal != null)
            {
                Console.WriteLine("Все существа в игре получили лечение");
                Heal();
            }
        }
    }

    abstract class Unit
    {
        protected int health;
        protected string name;
        public int Health { get => health; }
        public string Name { get => name; }
        public string Condition
        {
            get
            {
                if (health > 0)
                {
                    return $"Юнит: {Name} || здоровье: {Health}";   
                }
                else
                {
                    return $"Юнит {Name} уничтожен(на)";
                }
            }
        }

        protected Unit()
        {
            health = 1;
            name = "unit";
        } 

        public abstract void OnAttack();
        public abstract void OnHeal();
    }


    class Human : Unit
    {
        public Human() : base()
        {
            Random rr = new Random();
            health = rr.Next(20, 30);
            name = "human";
        }

        public Human(string str) : this()
        {
            name = str;
        }

        public override void OnAttack()
        {
            if (health > 0)
            {
                health -= 10;
                Console.WriteLine($"Человек {name} получил урон");
                if (health <= 0)
                {
                    Console.WriteLine($"Человек {name} умер");
                }
            }
            else
            {
                Console.WriteLine($"Атака по человеку {name} не проходит - он уже мертв");
            }
        }

        public override void OnHeal()
        {
            if (health > 0)
            {
                health += 10;
                Console.WriteLine($"Здоровье человека {name} увеличилось на 10");
            }
            else
            {
                Console.WriteLine($"Человека {name} нельзя полечить - он уже мертв");
            }
        }
    }

    class Undead : Unit
    {
        public Undead() : base ()
        {
            Random rr = new Random();
            health = rr.Next(2000, 3000);
            name = "undead";
        }

        public Undead (string str) : this()
        {
            name = str;
        }

        public override void OnAttack()
        {
            if (health > 0)
            {
                health -= 10;
                Console.WriteLine($"Нежить {name} получила урон");
                if (health <= 0)
                {
                    Console.WriteLine($"Нежить {name} уничтожена");
                }
            }
            else
            {
                Console.WriteLine($"Атака по нежити {name} не проходит, так как она уничтожена");
            }
        }

        public override void OnHeal()
        {
            if (health > 0)
            {
                health -= 1000;
                Console.WriteLine($"Нежить {name} получила урон от целительной силы лечения");
                if (health <= 0)
                {
                    Console.WriteLine($"Целительная сила лечения уничтожила нежить {name}");
                }
            }
            else
            {
                Console.WriteLine($"Лечение должно было убить нежить {name}, но она уже уничтожена");
            }
        }
    }
    class Fox : Unit
    {
        public Fox() : base() 
        {
            Random rr = new Random();
            health = rr.Next(2, 5);
            name = "foxy";
        }
        public Fox (string str) : this ()
        {
            name = str;
        }

        public override void OnAttack()
        {
            if (health > 0)
            {
                health --;
                Console.WriteLine($"Лисичка {name} получила урон");
                if (health <= 0)
                {
                    Console.WriteLine($"Лисичка {name} умерла :(");
                }
            }
            else
            {
                Console.WriteLine($"Лисичка {name} умерла, зачем ее бить снова?");
            }
        }

        public override void OnHeal()
        {
            if (health > 0)
            {
                health++;
                Console.WriteLine($"Здоровье лисички {name} увеличилось на 1 :)");
            }
            else
            {
                Console.WriteLine($"Лечи, не лечи - лисичка {name} уже умерла, тут ничего не поделаешь");
            }
        }
    }

    class Buildyng : Unit
    {

        public Buildyng()
        {
            Random rr = new Random();
            health = rr.Next(300, 800);
            name = "buildyng";
        }
        public Buildyng(string str) : this()
        {
            name = str;
        }
        public override void OnAttack()
        {
            if (health > 0)
            {
                health -= 150;
                Console.WriteLine($"Здание {name} получило повреждения разрушено");
                if (health <= 0)
                {
                    Console.WriteLine($"Здание {name} разрушено");
                }
            }
            else
            {
                Console.WriteLine($"Здание {name} уже разрушено, урон уходит в никуда");
            }
        }
        public override void OnHeal() { }
    }

    static class ExtentedStr
    {
        public static string UpperCase(this string str)
        {
            return str.ToUpper();
        }

        public static string AddSymbol(this string str)
        {
            return str + '!';
        }

        public static void Print(this string str)
        {
            Console.WriteLine(str);
        }
    } 


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myGame = new Game();

            var zombie = new Undead("Шон");
            var skeleton = new Undead("Костик");
            var man = new Human("Антон");
            var woman = new Human("Женя");
            var foxy = new Fox("Лис");
            var fox = new Fox("Пес");
            var house = new Buildyng();

            Unit[] units = { zombie, skeleton, man, woman, foxy, fox, house};

            foreach (Unit unit in units)
            {
                myGame.Attack += unit.OnAttack;
                myGame.Heal += unit.OnHeal;
            }

            myGame.CommandAttack();
            Console.WriteLine();
            myGame.CommandAttack();
            Console.WriteLine();
            myGame.CommandHeal();
            Console.WriteLine();
            myGame.CommandAttack();
            Console.WriteLine();
            myGame.CommandHeal();
            Console.WriteLine();
            myGame.CommandAttack();
            Console.WriteLine();

            Console.WriteLine("Состояние объектов в игре: ");
            foreach (Unit unit in units)
            {
                Console.WriteLine(unit.Condition);
            }
            Console.WriteLine();


            Action<string> action = ExtentedStr.Print;
            Func<string, string> func = ExtentedStr.UpperCase;
            Func<string, string> func1 = ExtentedStr.AddSymbol;

            action("Hello");
            Console.WriteLine(func("Aaaaaa"));
            Console.WriteLine(func1("Aaaaaa"));

        }
    }
}
