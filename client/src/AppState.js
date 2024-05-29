import { reactive } from 'vue'
import { Keep } from './models/Keep.js'
import { Vault } from './models/Vault.js'
import { Account } from './models/Account.js'
import { VaultKeep } from './models/VaultKeep.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /**@type {Account} */
  activeProfile: null,

  /**@type {Keep[]} */
  keeps: [],

  /**@type {Keep[]} */
  activeKeeps: [],

  /**@type {Keep} */
  activeKeep: null,

  /**@type {Vault[]} */
  activeVaults: [],
  
  /**@type {Vault[]} */
  myVaults: [],

  /**@type {Vault} */
  activeVault: null,

  /**@type {VaultKeep[]} */
  vaultKeeps: [],
  
})