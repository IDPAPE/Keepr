import { AppState } from "../AppState.js"
import { Account } from "../models/Account.js"
import { api } from "./AxiosService.js"

class ProfilesService{

    async getActiveProfile(profileId){
        AppState.activeProfile = null
        const response = await api.get(`api/profiles/${profileId}`)
        AppState.activeProfile = new Account(response.data)
    }
}

export const profilesService = new ProfilesService()