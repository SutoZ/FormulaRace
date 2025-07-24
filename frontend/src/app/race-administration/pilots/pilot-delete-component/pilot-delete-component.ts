import { Component } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { PilotsService } from '../../services/pilotservice';

@Component({
  selector: 'app-pilot-delete-component',
  imports: [MatDialogModule],
  templateUrl: './pilot-delete-component.html',
  styleUrl: './pilot-delete-component.css',
})
export class PilotDeleteComponent {
  constructor(private readonly pilotsService: PilotsService) {}

  deletePilot(id: number) {
    this.pilotsService.deletePilot(id).subscribe({
      next: () => {
        console.log(`Pilot with ID ${id} deleted successfully.`);
      },
      error: (error) => {
        console.error(`Error deleting pilot with ID ${id}:`, error);
      },
    });
  }
}
