using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AllErc1155Example : MonoBehaviour
{
    private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }
    public int[] AllNFTtokenID;
    public async void getAllNFTs()
    {
        string chain = "binance";
        string network = "testnet"; // mainnet goerli
        string account = "0x5Cc62E1F62539538432d3Fc13501F09f7c22BF2b";
        string contract = "0x1ED5Ab9697B26BFe97c85E6744B364FFD88D8814";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc1155(chain, network, account, contract, first, skip);
        try
        {
            
            NFTs[] erc1155s = JsonConvert.DeserializeObject<NFTs[]>(response);
            AllNFTtokenID = new int[10];
            int i = 0;
            foreach (NFTs erc1155 in erc1155s)
            {
                AllNFTtokenID [i] = int.Parse(erc1155.tokenId);
                if (int.Parse(erc1155.balance) > 0)
                    AllNFTtokenID [i] = int.Parse(erc1155.tokenId);
                Debug.Log(AllNFTtokenID);
                Debug.Log("contract : " + erc1155.contract);
                Debug.Log("tokenId : " + erc1155.tokenId);
                Debug.Log("uri : " + erc1155.uri);
                Debug.Log("balance : " + erc1155.balance);
                i += 1;

                Debug.Log(AllNFTtokenID.Length);
            }

            // for (int i = 0; i < erc1155s.Count; i++) {
            //     Debug.Log("contract : " + erc1155s[1].contract);
            //     Debug.Log("tokenId : " + erc1155s[1].tokenId);
            //     Debug.Log("uri : " + erc1155s[1].uri);
            //     Debug.Log("balance : " + erc1155s[1].balance);
            // }
        }
        catch
        {
           print("Error: " + response);
        }
    }
}
