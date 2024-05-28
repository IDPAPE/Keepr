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
    async getActiveVault(vaultId) {
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${vaultId}`)
        console.log('active vault response', response.data)
        AppState.activeVault = new Vault(response.data)
    }

    async getProfileVaults(profileId){
        AppState.activeVaults = null
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.activeVaults = response.data
    }
}

export const vaultsService = new VaultsService()