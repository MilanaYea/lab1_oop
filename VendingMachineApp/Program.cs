//вендинговый автомат

using System;



namespace VendingMachineApp {

    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Name} - {Price} руб (осталось {Quantity})";
        }
    }

    
    class VendingMachine
    {
        private List<Product> products;
        private Dictionary<int, int> coins;
        private decimal insertedAmount = 0;
        private decimal totalEarnings = 0;

        public VendingMachine()
        {
            
            products = new List<Product>
            {
                new Product("Кофе", 120, 10),
                new Product("Чай", 100, 10),
                new Product("Сникерс", 150, 10),
                new Product("Вода", 80, 10)
            };

            
            coins = new Dictionary<int, int>
            {
                { 10, 1000 },
                { 20, 1000 },
                { 50, 1000 },
                { 100, 1000 }
            };
        }

        
        public void DisplayProducts()
        {
            Console.WriteLine("\nСписок товаров:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i]}");
            }
        }

        public void InsertCoin(int val)
        {
            if (coins.ContainsKey(val))
            {
                coins[val]++;
                insertedAmount += val;
                Console.WriteLine($"Внесено: {val} руб | Всего: {insertedAmount} руб");
            }
            else
            {
                Console.WriteLine("Такой монеты автомат не принимает");
            }
        }

        public void SelectProduct(int index)
        {
            if (index < 1 || index > products.Count)
            {
                Console.WriteLine("Неверный номер товара");
                return;
            }

            Product selected = products[index - 1];

            if (selected.Quantity <= 0)
            {
                Console.WriteLine("Товар закончился");
                return;
            }

            if (insertedAmount >= selected.Price)
            {
                selected.Quantity--;
                insertedAmount -= selected.Price;
                totalEarnings += selected.Price;
                Console.WriteLine($"Вы получили: {selected.Name}");
                GiveChange(insertedAmount);
                insertedAmount = 0;
            }
            else
            {
                Console.WriteLine("Недостаточно средств");
            }
        }

        public void GiveChange(decimal change)
        {
            if (change > 0)
                Console.WriteLine($"Ваша сдача: {change} руб");
        }

        public void ReturnCoins()
        {
            if (insertedAmount > 0)
            {
                Console.WriteLine($"Возврат: {insertedAmount} руб");
                insertedAmount = 0;
            }
            else
            {
                Console.WriteLine("Вы ещё не вносили деньги");
            }
        }

        public void adminMode()
        {
            Console.Write("\nВведите пароль администратора: ");
            string password = Console.ReadLine();

            if (password != "1234")
            {
                Console.WriteLine("Неверный пароль");
                return;
            }

            int choice;
            do
            {
                Console.WriteLine("\n--- Админ-меню ---");
                Console.WriteLine("1. Пополнить товары");
                Console.WriteLine("2. Собрать выручку");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RefillProducts();
                        break;
                    case 2:
                        result();
                        break;
                }
            } while (choice != 0);
        }


        private void RefillProducts()
        {
            foreach (var product in products)
            {
                Console.Write($"Добавить товар {product.Name}: ");
                int qty = int.Parse(Console.ReadLine());
                product.Quantity += qty;
            }
            Console.WriteLine("Пополнение завершено");
        }

        private void result()
        {
            Console.WriteLine($"Получено: {totalEarnings} руб");
            totalEarnings = 0;
        }
    }

   
   
    class Program
    {
        static void Main()
        {
            VendingMachine machine = new VendingMachine();
            int choice;

            do
            {
                Console.WriteLine("\n--- ВЕНДИНГОВЫЙ АВТОМАТ ---");
                Console.WriteLine("1. Посмотреть товары");
                Console.WriteLine("2. Внести монеты");
                Console.WriteLine("3. Выбрать товар");
                Console.WriteLine("4. Отменить и вернуть деньги");
                Console.WriteLine("5. Войти как администратор");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        machine.DisplayProducts();
                        break;
                    case 2:
                        Console.Write("Введите номинал монеты (10, 20, 50, 100): ");
                        int coin = int.Parse(Console.ReadLine());
                        machine.InsertCoin(coin);
                        break;
                    case 3:
                        machine.DisplayProducts();
                        Console.Write("Введите номер товара: ");
                        int productIndex = int.Parse(Console.ReadLine());
                        machine.SelectProduct(productIndex);
                        break;
                    case 4:
                        machine.ReturnCoins();
                        break;
                    case 5:
                        machine.adminMode();
                        break;
                }

            } while (choice != 0);

            Console.WriteLine("Спасибо за использование автомата!");
        }
    }
}
