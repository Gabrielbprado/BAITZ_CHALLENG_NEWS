import { Component } from '@angular/core';
import { news } from '../../../interfaces/news';
import { NewsService } from '../../../shared/components/news/news.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-news-details',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './news-details.component.html',
  styleUrl: './news-details.component.css'
})
export class NewsDetailsComponent {

  news!: news;
  id: number = 0;
  liked: boolean = false; 

  constructor(private newsService: NewsService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const idParam = params.get('id'); 

      if (idParam) {
        this.id = +idParam; 

        this.newsService.GetById(this.id).subscribe(news => {
          this.news = news;
          console.log(this.news);
          
        });
      }
    });
  }

  onLike() {
    this.liked = true; 
    this.newsService.DoLike(this.id).subscribe(() => {
      console.log('Curtiu');
    });
  }

   onDislike() {
  //   this.liked = false;
  //   this.newsService.DoDislike(this.id).subscribe(() => {
  //     
  //   });
   }
}
