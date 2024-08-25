import { Component, Inject, LOCALE_ID, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrderBook } from '../../core/models/OrderBook';
import { OrderBookService } from '../../core/services/order-book.service';
import { MatTabChangeEvent, MatTabsModule } from '@angular/material/tabs';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Side } from '../../core/enums/Side';
import { CommonModule, registerLocaleData } from '@angular/common';
import { LoadingComponent } from '../../shared/loading/loading.component';
import { SecurityService } from '../../core/services/security.service';
import localePt from '@angular/common/locales/pt';
import { MatIconModule } from '@angular/material/icon';

registerLocaleData(localePt, 'pt-BR');

export interface DialogData {
  symbol: string;
  cryptoId: string;
  name: string;
  exchangeToBuyId: string;
  exchangeToBuyName: string;
  exchangeToSellId: string;
  exchangeToSellName: string;
  spread: number;
}

@Component({
  selector: 'app-order-book',
  standalone: true,
  imports: [
    MatTabsModule,
    MatTableModule,
    MatIconModule,
    CommonModule,
    LoadingComponent,
  ],
  templateUrl: './order-book.component.html',
  styleUrl: './order-book.component.css',
  providers: [{ provide: LOCALE_ID, useValue: 'pt-BR' }],
})
export class OrderBookComponent implements OnInit {
  orderBooksToBuy: OrderBook[] = [];
  orderBooksToSell: OrderBook[] = [];
  dsOrderBooksToBuy: MatTableDataSource<OrderBook> =
    new MatTableDataSource<OrderBook>();
  dsOrderBooksToSell: MatTableDataSource<OrderBook> =
    new MatTableDataSource<OrderBook>();
  displayedColumns: string[] = ['price', 'quantity', 'total', 'totalAmount'];

  selectedTabIndex = 0;

  constructor(
    private orderBookService: OrderBookService,
    private securityService: SecurityService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {}

  ngOnInit(): void {
    this.loadOrderBookToBuy();

    this.loadOrderBookToSell();
  }

  private loadOrderBookToBuy() {
    this.orderBookService
      .getAll(this.data.cryptoId, this.data.exchangeToBuyId, Side.SELL)
      .subscribe(
        (orderbook) => {
          this.orderBooksToBuy = orderbook.filter(
            (ob) => ob.side === Side.SELL
          );

          this.dsOrderBooksToBuy = new MatTableDataSource(this.orderBooksToBuy);
        },
        (error) => {
          if (error.status == 401) this.securityService.logOutToken();
        }
      );
  }

  private loadOrderBookToSell() {
    this.orderBookService
      .getAll(this.data.cryptoId, this.data.exchangeToSellId, Side.BUY)
      .subscribe(
        (orderbook) => {
          this.orderBooksToSell = orderbook.filter(
            (ob) => ob.side === Side.BUY
          );

          this.dsOrderBooksToSell = new MatTableDataSource(
            this.orderBooksToSell
          );
        },
        (error) => {
          if (error.status == 401) this.securityService.logOutToken();
        }
      );
  }

  onTabChange(event: MatTabChangeEvent) {
    this.selectedTabIndex = event.index;
  }
}
