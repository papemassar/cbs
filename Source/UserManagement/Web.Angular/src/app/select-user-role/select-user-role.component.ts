import { Component, OnInit } from '@angular/core';

const links = [
  { label: 'Administrator', href: 'admin' },
  { label: 'Data Verifier', href: 'data-verifier' },
  { label: 'Data Consumer', href: 'data-consumer' },
  { label: 'Data Coordinator', href: 'data-coordinator' },
  { label: 'System Configurator', href: 'system-configurator' },
  { label: 'Data Owner', href: 'data-owner' }
]

@Component({
  selector: 'cbs-select-user-role',
  templateUrl: './select-user-role.component.html',
  styleUrls: ['./select-user-role.component.scss']
})
export class SelectUserRoleComponent implements OnInit {
  links;

  constructor() {
    this.links = links;
  }

  ngOnInit() {}
}
