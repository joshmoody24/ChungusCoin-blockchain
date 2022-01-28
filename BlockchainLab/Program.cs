using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace BlockchainLab
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain chungusCoin = new Blockchain(6, 100);

            Console.WriteLine("Mining started");
            chungusCoin.MinePendingTransactions(wallet1);
            Console.WriteLine("Balance of wallet1 is $" + chungusCoin.GetBalanceOfWallet(wallet1));

            Console.WriteLine("Wallet1 is paying $10 to Wallet2");
            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            Console.WriteLine("Wallet2 is mining a new block with that transaction");
            chungusCoin.addPendingTransaction(tx1);
            chungusCoin.MinePendingTransactions(wallet2);

            string blockchainJSON = JsonConvert.SerializeObject(chungusCoin, Formatting.Indented);
            Console.WriteLine(blockchainJSON);

            Console.WriteLine("Wallet1 balance: $" + chungusCoin.GetBalanceOfWallet(wallet1));
            Console.WriteLine("Wallet2 balance: $" + chungusCoin.GetBalanceOfWallet(wallet2));

            if (chungusCoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is valid");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid");
            }
        }
    }
}
