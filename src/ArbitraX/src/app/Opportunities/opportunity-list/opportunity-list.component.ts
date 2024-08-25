import { OpportunityService } from './../../core/services/opportunity.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { LoadingComponent } from '../../shared/loading/loading.component';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { OpportunityViewModel } from '../../core/ViewModels/OpportunityViewModel';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog } from '@angular/material/dialog';
import { OrderBookComponent } from '../order-book/order-book.component';
import { SecurityService } from '../../core/services/security.service';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-opportunity-list',
  standalone: true,
  imports: [
    MatTableModule,
    LoadingComponent,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './opportunity-list.component.html',
  styleUrl: './opportunity-list.component.css',
})
export class OpportunityListComponent implements OnInit {
  constructor(
    private opportunityService: OpportunityService,
    private securityService: SecurityService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  opportunities: OpportunityViewModel[] = [];
  dsOpportunities: MatTableDataSource<OpportunityViewModel> =
    new MatTableDataSource<OpportunityViewModel>();
  displayedColumns: string[] = [
    'position',
    'symbol',
    'date',
    'valueToBuy',
    'valueToSell',
    'fee',
    'differencePercentage',
    'exchangeToBuy',
    'exchangeToSell',
    'showOrderBook',
  ];
  expandedElement: OpportunityViewModel | null;

  @ViewChild('opportunitiesPaginator') opportunitiesPaginator: MatPaginator;

  loadingOpportunities: boolean = false;
  noneOpportunity: boolean = false;

  dolar: number = 0;

  private intervalId: any;

  ngOnInit(): void {
    this.loadOpportunities();

    this.intervalId = setInterval(() => {
      this.loadOpportunities();
    }, 30000);
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  protected showOrderBooks(
    cryptoSymbol: string,
    cryptoId: string,
    name: string,
    exchangeToBuyId: string,
    exchangeToBuyName: string,
    exchangeToSellId: string,
    exchangeToSellName: string,
    spread: number
  ) {
    this.dialog.open(OrderBookComponent, {
      data: {
        symbol: cryptoSymbol,
        cryptoId: cryptoId,
        name: name,
        exchangeToBuyId: exchangeToBuyId,
        exchangeToBuyName: exchangeToBuyName,
        exchangeToSellId: exchangeToSellId,
        exchangeToSellName: exchangeToSellName,
        spread: spread,
      },
    });
  }

  private loadOpportunities() {
    this.loadingOpportunities = true;
    this.opportunityService.getAll().subscribe(
      (opportunities) => {
        this.opportunities = opportunities.opportunities;
        this.dsOpportunities = new MatTableDataSource(this.opportunities);
        this.dsOpportunities.paginator = this.opportunitiesPaginator;

        this.dolar = opportunities.dolar;

        if (this.opportunities.length == 0) this.noneOpportunity = true;

        this.loadingOpportunities = false;
      },
      (error) => {
        if (error.status == 401) {
          this.securityService.logOutToken();
        } else
          this.openSnackBar('Erro ao consultar as oportunidades: ' + error);

        this.loadingOpportunities = false;
      }
    );
  }

  private openSnackBar(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 3000,
    });
  }

  protected applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dsOpportunities.filter = filterValue.trim().toLowerCase();
  }

  isExpansionDetailRow = (i: number, row: Object) =>
    row.hasOwnProperty('detailRow');
}
