export interface OpportunityViewModel {
  position: number;
  valueToBuy: number;
  valueToSell: number;
  differencePercentage: number;
  cryptoId: string;
  symbol: string;
  name: string;
  exchangeToBuyId: string;
  exchangeToBuyName: string;
  exchangeToSellId: string;
  exchangeToSellName: string;
  date: Date;
  exchangeToBuyUrl: string;
  exchangeToSellUrl: string;
}

export interface GetAllResponse {
  opportunities: OpportunityViewModel[];
  dolar: number;
}
