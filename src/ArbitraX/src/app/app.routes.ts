import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CryptoAdmComponent } from './Adm/crypto-adm/crypto-adm.component';
import { ExchangeAdmComponent } from './Adm/exchange-adm/exchange-adm.component';
import { UserAdmComponent } from './Adm/user-adm/user-adm.component';
import { ConfigAdmComponent } from './Adm/config-adm/config-adm.component';
import { OpportunityListComponent } from './Opportunities/opportunity-list/opportunity-list.component';
import { ExchangeWithdrawalFeeComponent } from './Adm/exchange-withdrawal-fee/exchange-withdrawal-fee.component';

export const routes: Routes = [
  {
    path: '',
    component: OpportunityListComponent,
    data: { title: 'Oportunidades' },
  },
  {
    path: 'crypto-adm',
    component: CryptoAdmComponent,
    data: { title: 'Criptomoedas' },
  },
  {
    path: 'exchange-adm',
    component: ExchangeAdmComponent,
    data: { title: 'Corretoras' },
  },
  {
    path: 'exchange-withdrawal-fee',
    component: ExchangeWithdrawalFeeComponent,
    data: { title: 'Taxas de Transferência' },
  },
  {
    path: 'user-adm',
    component: UserAdmComponent,
    data: { title: 'Usuários' },
  },
  {
    path: 'config-adm',
    component: ConfigAdmComponent,
    data: { title: 'Configurações' },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
