import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { router } from "../router.js"
import { api } from "./AxiosService.js"

class VaultsService{
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        console.log('new vault response', response.data)
        AppState.myVaults.push(new Vault(response.data))
    }
    
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
        AppState.activeKeep.kept ++
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

    async deleteVault(vaultId) {
        const indexToDelete = AppState.myVaults.findIndex(vault => vault.id == vaultId)
        if (indexToDelete == -1){
            throw new Error(`could not find ${vaultId} in myVaults, check ur code`)
        }
        await api.delete(`api/vaults/${vaultId}`)
        AppState.myVaults.splice(indexToDelete, 1)
    }
}

export const vaultsService = new VaultsService()