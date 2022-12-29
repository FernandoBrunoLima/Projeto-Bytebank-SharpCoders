using System.Reflection.Metadata.Ecma335;

namespace ByteBank1 {

    public class Program {

        static void ShowMenu() {
            
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Transações bancárias");
            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            
        }

        static void ShowSubMenu()
        {
            Console.WriteLine("1 - Efetuar depósito");
            Console.WriteLine("2 - Efetuar transferência");
            Console.WriteLine("3 - Efetuar saque");
            Console.WriteLine("0 - Voltar para o menu principal: ");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
        }

        static void EnrollNewUser(List<string> cpfs, List<string> holders, List<string> passwords , List<double> balances) {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            holders.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            passwords.Add(Console.ReadLine());
            balances.Add(0);
        }

        static void DeleteUser(List<string> cpfs, List<string> holders, List<string> passwords, List<double> balances) {
            Console.Write("Digite o cpf: ");
            string cpfTyped = Console.ReadLine();
            int indexFoundByCpf = cpfs.FindIndex(cpf => cpf == cpfTyped);
          
            if(indexFoundByCpf == -1) {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: conta não encontrada.");
            }

            cpfs.Remove(cpfTyped);
            holders.RemoveAt(indexFoundByCpf);
            passwords.RemoveAt(indexFoundByCpf);
            balances.RemoveAt(indexFoundByCpf);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListAllAccounts(List<string> cpfs, List<string> holders, List<double> balances) {
            for(int i = 0; i < cpfs.Count; i++) {
                ShowAccount(i, cpfs, holders, balances);
            }
        }

        static void ShowUser(List<string> cpfs, List<string> holders, List<double> balances) {
            Console.Write("Digite o cpf: ");
            string cpfTyped = Console.ReadLine();
            int indexFoundByCpf = cpfs.FindIndex(cpf => cpf == cpfTyped);

            if (indexFoundByCpf == -1) {
                Console.WriteLine("Não foi possível exibir esta conta");
                Console.WriteLine("MOTIVO: conta não encontrada.");
            }

            ShowAccount(indexFoundByCpf, cpfs, holders, balances);
        }

        static void ShowAccumulatedValue(List<double> balances) {
            Console.WriteLine($"Total acumulado no banco: {balances.Sum()}");
            // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
        }

        static void ShowAccount( int index, List<string> cpfs, List<string> holders, List<double> balances) {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {holders[index]} | Saldo = R${balances[index]:F2}");
        }


        static void MakeDeposit(List<string> cpfs, List<string> holders, List<double> balances)
        {
            Console.Write("Digite o cpf: ");
            string cpfTyped = Console.ReadLine();
            int indexFoundByCpf = cpfs.FindIndex(cpf => cpf == cpfTyped);
            double amount = 0;
            if (indexFoundByCpf == -1)
            {
                Console.WriteLine("Não foi possível realizar o depósito");
                Console.WriteLine("MOTIVO: conta não encontrada.");
            }
            else
            {
                Console.Write("Digite o valor para depósito: ");
                amount = double.Parse(Console.ReadLine());
                balances[indexFoundByCpf] += amount;
                Console.WriteLine($"Depósito realizado: {amount:F2}");
                Console.WriteLine($"Saldo atual: {balances[indexFoundByCpf]:F2}");
            }


        }
        static void MakeWithdrawal(List<string> cpfs, List<string> holders, List<double> balances)
        {
            Console.Write("Digite o cpf: ");
            string cpfTyped = Console.ReadLine();
            int indexFoundByCpf = cpfs.FindIndex(cpf => cpf == cpfTyped);
            double amount = 0;

            if (indexFoundByCpf == -1)
            {
                Console.WriteLine("Não é possível efetuar saque");
                Console.WriteLine("MOTIVO: conta não encontrada.");
            }
            else
            {
                Console.Write("Digite o valor para saque: ");
                amount = double.Parse(Console.ReadLine());
            }

            if (balances[indexFoundByCpf] >= amount)
            {
                balances[indexFoundByCpf] -= amount;
                Console.WriteLine($"Saque efetuado: {amount:F2}");
                Console.WriteLine($"Saldo atual: {balances[indexFoundByCpf]:F2}");
            }
            else
            {
                Console.WriteLine("Saque não realizado");
                Console.WriteLine("MOTIVO: saldo insuficiente");
            }
        }
        static void MakeTransfer(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf do usuário da conta: ");
            string cpfTyped = Console.ReadLine();
            int indexFoundByCpf = cpfs.FindIndex(cpf => cpf == cpfTyped);
            Console.Write("Digite o cpf da conta do credor: ");
            string payeeCpf = Console.ReadLine();
            int payeeIndex = cpfs.FindIndex(cpf => cpf == payeeCpf);

            double amount = 0;
            if (indexFoundByCpf == -1 || payeeIndex == -1)
            {
                Console.WriteLine("Transferência não realizada");
                Console.WriteLine("MOTIVO: conta(s) não encontrada(s).");
            }
            else
            {
                Console.Write("Digite o valor: ");
                amount = double.Parse(Console.ReadLine());
            }

            if (saldos[indexFoundByCpf] < amount)
            {
                Console.WriteLine("Transferência não realizada");
                Console.WriteLine("MOTIVO: saldo insuficiente.");
            }
            else
            {
                saldos[indexFoundByCpf] -= amount;
                saldos[payeeIndex] += amount;
                Console.WriteLine($"Transferencia Realizada no valor de {amount:F2}");
            }
        }

        static void bankTransactions(List<string> cpfs, List<string> holders, List<double> balances)
        {

            int option;

            do {
                ShowSubMenu();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        MakeDeposit(cpfs, holders, balances);
                        break;
                    case 2:
                        MakeWithdrawal(cpfs, holders, balances);
                        break;
                    case 3:
                        MakeTransfer(cpfs, holders, balances);
                        break;
                    
                }


            } while (option != 0) ;
        }

        public static void Main(string[] args) {

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Menu Principal - Banco ByteBank");
            Console.WriteLine();

            List<string> cpfs = new List<string>();
            List<string> holders = new List<string>();
            List<string> passwords = new List<string>();
            List<double> balances = new List<double>();

            int option = 0;

            do {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option) {
                    case 0:
                        Console.WriteLine("Encerrando o programa...");
                        break;
                    case 1:
                        EnrollNewUser(cpfs, holders, passwords, balances);
                        break;
                    case 2:
                        DeleteUser(cpfs, holders, passwords, balances);
                        break;
                    case 3:
                        ListAllAccounts(cpfs, holders, balances);
                        break;
                    case 4:
                        ShowUser(cpfs, holders, balances);
                        break;
                    case 5:
                        ShowAccumulatedValue(balances);
                        break;
                    case 6:
                        bankTransactions(cpfs, holders, balances);
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);
            
            

        }

    }

}