using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GetAllNFTs : MonoBehaviour
{
    
    static string chain = "binance";
    static string network = "mainnet"; 
    public static int [] AllNFTsTokenID;
    static int index;

    private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }

    void Start() {
        AllNFTsTokenID = new int [100];
    }

    public async static void getDexNFTs()
    {
        index = 0;   
        string account = PlayerPrefs.GetString("Account");
        string contract = "0x76A28d82B62Aa454A3f366C5F08ce43355849897";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc1155(chain, network, account, contract, first, skip);
        try
        {
            AllNFTsTokenID = new int [100];
            NFTs[] erc1155s = JsonConvert.DeserializeObject<NFTs[]>(response);
            index = 0;
            foreach (NFTs erc1155 in erc1155s)
            {
                if (int.Parse(erc1155.balance) > 0)
                    AllNFTsTokenID[index] = int.Parse(erc1155.tokenId);
                index += 1;
            }
            getBNBNFTs();
        }
        catch
        {
           print("Error: " + response);
        }
    }

    public async static void getBNBNFTs()
    {
        
        string account = PlayerPrefs.GetString("Account");
        string contract = "0x3C7DA571076b3d72489569ac2A286E5D066b4866";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc1155(chain, network, account, contract, first, skip);
        try
        {
            NFTs[] erc1155s = JsonConvert.DeserializeObject<NFTs[]>(response);
            foreach (NFTs erc1155 in erc1155s)
            {
                bool flag = true;                
                if (int.Parse(erc1155.balance) > 0) {
                    for (int i = 0; i < index; i++){
                        if (AllNFTsTokenID[i] == int.Parse(erc1155.tokenId)) {
                            flag = false;
                        }
                    }   
                    if (flag)
                        AllNFTsTokenID[index] = int.Parse(erc1155.tokenId);                 
                }
                index += 1;
            }
        }
        catch
        {
           print("Error: " + response);
        }
    }
}
