import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-generic-table',
  templateUrl: './generic-table.component.html',
  styleUrls: ['./generic-table.component.css']
})
export class GenericTableComponent {
  @Input() dataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  @Input() displayedColumns: string[] = [];
  @Input() totalRecords: number = 0;
  @Input() pageSizeOptions: number[] = [10, 25, 50, 100];
  @Input() pageSize: number = 10; // Agrega esta línea

  @Output() pageChange = new EventEmitter<any>();
  @Output() rowAction = new EventEmitter<any>();

  constructor() { }

  onPageChange(event: any) {
    this.pageChange.emit(event);
  }

  onRowAction(action: string, row: any) {
    this.rowAction.emit({ action, row });
  }
}
