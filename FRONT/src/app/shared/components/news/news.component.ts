import { Component, Input } from '@angular/core';
import { news } from '../../../interfaces/news';
import { Router } from '@angular/router';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [],
  templateUrl: './news.component.html',
  styleUrl: './news.component.css'
})
export class NewsComponent {

  @Input() news:news = {
    id: 1,
    title: 'Andrey Boot',
    author: 'Gabriel',
    createdAt: '25/12/2099',
    likes: 0,
    imageUrl: 'https://www.google.com',
    introduction: 'Andrey Boot is a great boot',
    content: 'Andrey Boot is a great boot',
    description: 'Andrey Boot is a great boot',
    topics: []
    
  }
  constructor(private route: Router) { }

  OpenNotice()
  {
    this.route.navigate(['/news-details', this.news.id]);
  }
}
