# Universal Profile Avatar SDK Example

A barebones example Unity project using the Universal Profile Avatar SDK.

Loads an Universal Profile and it's avatars, then displays them in the UI.

![Unity_2022-07-12_14-52-52](https://user-images.githubusercontent.com/16716633/178483863-3bf52719-e52e-4318-9d18-590098ef55c1.png)

The project was created in Unity **2020.3.30f1**, other versions were not tested.

## Getting Started
To get started clone the repo, load the Unity project and open the **SampleScene**.

You'll be presented with a scene that consists of a UI that you can interact with.
Enter a Universal Profile address or a local path to a [LSP3Profile JSON](https://github.com/lukso-network/LIPs/blob/main/LSPs/LSP-3-UniversalProfile-Metadata.md#lsp3profile) then load it.

Once loaded you'll see the profile name and description as well as a dropdown of available avatars you can load. By default only avatar bundles that match your current paltforms will show up in the dropdown, but that can be changed by ticking the `Filter to current platform` checkbox.
Once an avatar is selected you'll see some info about it, like it's hash function, hash, url and file type.

The `fileType` field is what's responsible for platform filtering. Currently, an entry in the avatar array uses the following format:

```json
{
	"hashFunction": "keccak256(bytes)",
	"hash": "0x6b9d7c98a455236845ef44e3f0120ff1d89301753b049d9230cd1f48869a708a",
	"url": "ipfs://QmRgpQnk82SMeTudCmunskDE98t81QiejvqEofNE48vSXc",
	"fileType": "assetbundle/windows"
}
```
