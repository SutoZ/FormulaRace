import { Component } from '@angular/core';
import { PilotsService } from '../../services/pilots.service';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-pilot-delete-component',
  templateUrl: './pilot-delete-component.html',
  styleUrl: './pilot-delete-component.css',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule
  ],
})
export class PilotDeleteComponent {

  constructor(private readonly pilotService: PilotsService) { }

  deletePilot(id: number) {
    this.pilotService.deletePilot(id).subscribe({
      next: () => console.log(`Pilot with id ${id} deleted`),
      error: err => console.error(err)
    });
  }
}
