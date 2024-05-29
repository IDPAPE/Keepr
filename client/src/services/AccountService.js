import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async updateAccountInfo(accountInfo){
    accountInfo.email = AppState.account.email
    const response = await api.put('/account', accountInfo)
    console.log('update account response', response.data)
    AppState.account = new Account(response.data)
    AppState.activeProfile = new Account(response.data)
  }
  
}

export const accountService = new AccountService()
