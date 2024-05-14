import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-row-actions',
  templateUrl: './row-actions.component.html',
  styleUrls: ['./row-actions.component.css']
})
export class RowActionsComponent {
  @Input() row: any;
  @Output() rowAction = new EventEmitter<string>();

  constructor() { }

  onAction(action: string) {
    this.rowAction.emit(action);
  }
}
