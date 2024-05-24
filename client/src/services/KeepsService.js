import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { api } from "./AxiosService.js"

class KeepsService{
    async getAllKeeps() {
        const response = await api.get('api/keeps')
        AppState.keeps = response.data.map(keep => new Keep(keep))
        console.log('Appstate keeps', AppState.keeps)
    }
    
    async setActiveKeep(keepId){
        const keep = AppState.keeps.find(keep => keep.id == keepId)
        AppState.activeKeep = keep
    }

    async deleteKeep(keepId) {
        const indexToDelete = AppState.keeps.findIndex(keep => keep.id == keepId)
        if(indexToDelete == -1)
            {
                throw new Error('find index failed')
            }
        await api.delete(`api/keeps/${keepId}`)
        AppState.keeps.splice(indexToDelete, 1)
    }
    
}
export const keepsService = new KeepsService()