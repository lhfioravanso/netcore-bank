import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from  '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { MatBadgeModule } from '@angular/material/badge';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatNativeDateModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
   imports: [
      CommonModule,
      MatBadgeModule,
      MatGridListModule,
      MatFormFieldModule,
      MatSelectModule,
      MatDatepickerModule,
      MatChipsModule,
      MatTooltipModule,
      MatToolbarModule,
      MatSidenavModule,
      MatListModule,
      MatButtonModule,
      MatIconModule,
      MatPaginatorModule,
      MatDialogModule,
      MatSortModule,
      MatTableModule,
      MatInputModule,
      MatRadioModule,
      MatCardModule,
      MatNativeDateModule,
      MatAutocompleteModule,
      MatExpansionModule
   ],
   exports: [
    MatBadgeModule,
    MatGridListModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatChipsModule,
    MatTooltipModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    MatDialogModule,
    MatSortModule,
    MatTableModule,
    MatInputModule,
    MatRadioModule,
    MatCardModule,
    MatNativeDateModule,
    MatAutocompleteModule,
    MatExpansionModule
   ],
   providers: [
      MatDatepickerModule,
   ]
})

export class AngularMaterialModule { }