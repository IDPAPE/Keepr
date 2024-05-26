import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class VaultsService{
    
    async getMyVaults(){
        const response = await api.get('account/vaults')
        console.log('my vaults response:', response.data)
        AppState.myVaults = response.data.map(vault => new Vault(vault))
        console.log('appstate myvaults:', AppState.myVaults)
    }
    async saveToVault(selectedVaultData) {
        selectedVaultData.keepId = AppState.activeKeep.id
        console.log('saving to vault body:', selectedVaultData)
        const response = await api.post(`api/vaultkeeps`, selectedVaultData)
        console.log('saving keep response data',response.data)
    }
}

export const vaultsService = new VaultsService()