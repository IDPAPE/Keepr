import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { api } from "./AxiosService.js"

class KeepsService{
    async createKeep(keepData) {
        // keepData.creatorId = AppState.account.id
        const response = await api.post('api/keeps', keepData)
        console.log('create keep response', response.data)
        AppState.activeKeeps.push(new Keep(response.data))
    }
    async getAllKeeps() {
        const response = await api.get('api/keeps')
        AppState.activeKeeps = response.data.map(keep => new Keep(keep))
        console.log('Appstate keeps', AppState.keeps)
    }
    
    async setActiveKeep(keepId){
        const keep = AppState.activeKeeps.find(keep => keep.id == keepId)
        const viewedKeep = await api.put(`api/keeps/${keepId}/viewed`)
        AppState.activeKeep = new Keep(viewedKeep.data)
    }

    async deleteKeep(keepId) {
        const indexToDelete = AppState.activeKeeps.findIndex(keep => keep.id == keepId)
        if(indexToDelete == -1)
            {
                throw new Error('find index failed')
            }
        await api.delete(`api/keeps/${keepId}`)
        AppState.activeKeeps.splice(indexToDelete, 1)
    }

    async removeKeepFromVault(vaultKeepId) {
        const indexToDelete = AppState.vaultKeeps.findIndex(keep => keep.vaultKeepId == vaultKeepId)
        if(indexToDelete == -1)
            {
                throw new Error('find index failed')
            }
            await api.delete(`api/vaultkeeps/${vaultKeepId}`)
            AppState.vaultKeeps.splice(indexToDelete, 1)
    }
    
    async getVaultKeeps(vaultId){
        AppState.vaultKeeps = []
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        console.log('vault keeps response', response.data)
        AppState.vaultKeeps = response.data.map(keep => new VaultKeep(keep))
    }

    async getProfileKeeps(profileId){
        AppState.activeKeeps = []
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.activeKeeps = response.data.map(keep => new Keep (keep))
        console.log('account keeps', AppState.activeKeeps)
    }
}
export const keepsService = new KeepsService()