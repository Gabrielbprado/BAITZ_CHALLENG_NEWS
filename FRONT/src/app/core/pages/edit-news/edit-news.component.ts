import { Component, Input, input, OnInit, ViewChild } from '@angular/core';
import { NewsFormComponent } from "../../../shared/components/news-form/news-form.component";
import { NewsService } from '../../../shared/components/news/news.service';
import { news } from '../../../interfaces/news';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-news',
  standalone: true,
  imports: [NewsFormComponent,CommonModule],
templateUrl: './edit-news.component.html',
  styleUrl: './edit-news.component.css'
})
export class EditNewsComponent implements OnInit {
  @ViewChild(NewsFormComponent) existingNewsFormComponent!: NewsFormComponent; // Referência ao componente do formulário
  news!: news;
  id: number = 0;

  constructor(private newsService: NewsService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const idParam = params.get('id'); 

      if (idParam) {
        this.id = +idParam; 

        this.newsService.GetById(this.id).subscribe(news => {
          this.news = news;
          console.log('Notícia carregada com sucesso', this.news);
          console.log(this.news);
          
          // Preenche o formulário assim que os dados são recebidos
          this.existingNewsFormComponent.updateForm(this.news);
        });
      }
    });
  }
}
