MenuPrincipal menu = new MenuPrincipal();
MenuListar menuListar = new MenuListar();
List<Responsavel> responsaveis = new List<Responsavel>();
List<Responsavel> lista = new List<Responsavel>();
string arquivo = "teste.txt";
string texto = "Uma coisa qualquer";
CSV csv = new CSV();

csv.EscreverDados(arquivo, texto);
csv.LerDados(arquivo, lista);
// do 
// {
//     menu.Mostrar();

//     switch (menu.opcao)
//     {
//         case 1:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 2:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 3:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 4:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 5:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 6:
//             System.Console.WriteLine(menu.opcao);
//             do {
//                 menuListar.Mostrar();
//                 switch (menuListar.opcao)
//                 {
//                     case 1:
//                         System.Console.WriteLine(menuListar.opcao);
//                         break;
//                     case 2:
//                         System.Console.WriteLine(menuListar.opcao);
//                         break;
//                     case 3:
//                         System.Console.WriteLine(menuListar.opcao);
//                         break;
//                 }
//             } while(menuListar.opcao != 0);
//             break;
//         case 7:
//             System.Console.WriteLine(menu.opcao);
//             break;
//         case 0:
//             System.Console.WriteLine("Até mais.");
//             break;
//     }
//     Thread.Sleep(3000);
// } while (menu.opcao != 0);

// Console.Clear();
