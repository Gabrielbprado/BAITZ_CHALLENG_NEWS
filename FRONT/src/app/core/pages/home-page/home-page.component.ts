import { Component, OnInit } from '@angular/core';
import { NewsComponent } from "../../../shared/components/news/news.component";
import { CommonModule } from '@angular/common';
import { news } from '../../../interfaces/news';
import { NewsService } from '../../../shared/components/news/news.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [NewsComponent, CommonModule, RouterModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  mainNews: news | undefined;
  isLoading: boolean = true;
  listNews: news[] = [];
  
  // Pagination properties
  currentPage: number = 1;
  itemsPerPage: number = 8;
  totalPages: number = 1;

  constructor(private service: NewsService) { }

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(): void {
    this.service.GetAll().subscribe({
      next: (listNews) => {
        console.log('Notícias carregadas com sucesso', listNews);
        this.listNews = listNews;
        this.totalPages = Math.ceil(this.listNews.length / this.itemsPerPage);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar as notícias', error);
        this.isLoading = false;
      }
    });

    this.service.GetNewsMoreLiked().subscribe({
      next: (mainNews) => {
        this.mainNews = mainNews;
        console.log('Notícia principal carregada com sucesso', mainNews.imageUrl);
      },
      error: (error) => {
        console.error('Erro ao carregar a notícia principal', error);
      }
    });
  }

  // Pagination methods
  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  get paginatedNews(): news[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.listNews.slice(start, end);
  }
}