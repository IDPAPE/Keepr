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

}
export const keepsService = new KeepsService()