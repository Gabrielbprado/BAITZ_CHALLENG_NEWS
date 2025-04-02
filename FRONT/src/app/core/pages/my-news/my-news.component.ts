import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../../shared/components/news/news.service';
import { news } from '../../../interfaces/news';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteModalComponent } from '../../../shared/modal/delete-modal/delete-modal.component';

@Component({
  selector: 'app-my-news',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-news.component.html',
  styleUrl: './my-news.component.css'
})
export class MyNewsComponent implements OnInit {

  listNews: news[] = [];
  
  constructor(private newsService: NewsService,private route: Router,private dialog: MatDialog) {}
  ngOnInit(): void {
    this.newsService.GetByUser().subscribe(news => {
      this.listNews = news;
    });
  }
  
  editNews(id: number) {

    this.route.navigate(['edit-news',id]);
  }

  openDeleteConfirmation(id: number): void {
    const dialogRef = this.dialog.open(DeleteModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.newsService.Delete(id).subscribe(() => {
          this.route.navigate(['home']);
        });
      } else {
        console.log('Exclusão cancelada');
      }
    });
  }
}
