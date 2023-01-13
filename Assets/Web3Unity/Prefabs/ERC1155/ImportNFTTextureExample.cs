using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ImportNFTTextureExample : MonoBehaviour
{

     private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }


    public class Response {
        public string image;
    }
    // async void Start()
    // {
    //     string chain = "binance";
    //     string network = "mainnet";
    //     string contract = "0x3C7DA571076b3d72489569ac2A286E5D066b4866";
    //     string tokenId = "10";

    //     // fetch uri from chain
    //     // string uri = await ERC1155.URI(chain, network, contract, tokenId);
    //     // print("uri: " + uri);

    //     // // fetch json from uri
    //     // UnityWebRequest webRequest = UnityWebRequest.Get(uri);
    //     // await webRequest.SendWebRequest();
    //     // Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

    //     // parse json to get image uri
    //     string imageUri = "ipfs://QmRdiZpf3B4dTA9F7LUtB6nyfc7VVCiKGVni1D2ZCFCNv3/10.png";
    //     print("imageUri: " + imageUri);
    //     if (imageUri.StartsWith("ipfs://"))
    //     {
    //         imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
    //     }
    //     Debug.Log("Revised URI: " + imageUri);
    //     // fetch image and display in game
    //     UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
    //     await textureRequest.SendWebRequest();
    //     gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
    // }

   

    async void Start()
    {
        string chain = "binance";
        string network = "mainnet"; // mainnet ropsten kovan rinkeby goerli
        string account = "0x97f5d69f3aab2b1de32cb74cfdd8e214cd9bf4a7";
        string contract = "0x3C7DA571076b3d72489569ac2A286E5D066b4866";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc1155(chain, network, account, contract, first, skip);
        try
        {
            NFTs[] erc1155s = JsonConvert.DeserializeObject<NFTs[]>(response);
            print(erc1155s[0].contract);
            print(erc1155s[0].tokenId);
            print(erc1155s[0].uri);
            print(erc1155s[0].balance);
        }
        catch
        {
           print("Error: " + response);
        }
    }
}
