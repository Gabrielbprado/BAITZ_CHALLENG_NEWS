import { Component, OnInit, Input } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NewsService } from '../news/news.service';
import { Router } from '@angular/router';
import { news } from '../../../interfaces/news';

@Component({
  selector: 'app-news-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './news-form.component.html',
  styleUrls: ['./news-form.component.css']
})
export class NewsFormComponent implements OnInit {
  @Input() existingNews: news | undefined; 
  isEditing = false; 
  newsForm: FormGroup = new FormGroup({});
  mainImagePreview: any;

  constructor(private fb: FormBuilder, private service: NewsService, private router: Router) {}

  ngOnInit(): void {
    this.newsForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      content: ['', Validators.required],
      author: [''],
      mainImage: [null],
      introduction: [''],
      topics: this.fb.array([]) 
  });
  

    if (this.existingNews) {
        this.updateForm(this.existingNews);
    }
}


updateForm(existingNews: news) {
    this.isEditing = true;
    this.newsForm.patchValue({
        title: existingNews.title,
        description: existingNews.description,
        content: existingNews.content,
        author: existingNews.author,
        introduction: existingNews.introduction,
    });

    this.mainImagePreview = existingNews.imageUrl;

    this.topics.clear(); 

    if (existingNews.topics && existingNews.topics.length) {
        existingNews.topics.forEach(topic => {
            const topicGroup = this.fb.group({
                topicTitle: [topic.title, Validators.required], 
                topicContent: [topic.description, Validators.required], 
                topicImage: [topic.imageUrl] 
            });

            this.topics.push(topicGroup);
        });
        console.log('Tópicos carregados:', this.topics.value); 
    } else {
        console.log('Nenhum tópico encontrado.'); 
    }
}

  get Topics(): FormArray {
    return this.newsForm.get('topics') as FormArray;
  }

  onFileChange(event: Event, controlName: string | number) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const reader = new FileReader();
  
      reader.onload = () => {
        if (typeof controlName === 'string') {
          this.mainImagePreview = reader.result;  
          this.newsForm.get(controlName)?.setValue(file);
        } else if (typeof controlName === 'number') {
          this.topics.at(controlName).get('topicImage')?.setValue(file);
        }
      };
  
      reader.readAsDataURL(file);
    }
  }
  
  getTopicImagePreview(index: number): any {
    const topic = this.topics.at(index);
    const file = topic.get('topicImage')?.value;
    
    if (typeof file === 'string') {
      return file; 
    } else if (file instanceof File) {
      return URL.createObjectURL(file); 
    }
    
    return null; 
  }

  get topics(): FormArray {
    return this.newsForm.get('topics') as FormArray;
  }
  
  addTopic(): void {
    const topicGroup = this.fb.group({
      topicTitle: ['', Validators.required],
      topicContent: ['', Validators.required],  
      topicImage: [null]
    });

    this.topics.push(topicGroup);
  }

  removeTopic(index: number): void {
    this.topics.removeAt(index);
  }

  onSubmit(): void {
    if (this.newsForm.valid) {
      const formData = new FormData();
      
      formData.append('Title', this.newsForm.value.title );
      formData.append('Content', this.newsForm.value.content );
      formData.append('Description', this.newsForm.value.description?.trim() === '' ? null : this.newsForm.value.description);
      formData.append('Author', this.newsForm.value.author?.trim() === '' ? null : this.newsForm.value.author);
      formData.append('Introduction', this.newsForm.value.introduction?.trim() === '' ? null : this.newsForm.value.introduction);
      
      const mainImage = this.newsForm.get('mainImage')?.value;
      if (mainImage) {
        formData.append('Image', mainImage);
      }

      this.topics.controls.forEach((topic, index) => {
        formData.append(`Topics[${index}].Title`, topic.get('topicTitle')?.value);
        formData.append(`Topics[${index}].Description`, topic.get('topicContent')?.value);  
        
        const topicImage = topic.get('topicImage')?.value;
        if (topicImage) {
          formData.append(`Topics[${index}].Image`, topicImage);
        }
      });

      if (this.isEditing) {

        this.service.updateNews(formData,this.existingNews?.id).subscribe(
          (response) => {
            console.log(response);
            this.router.navigate(['/home']);
          },
          (error) => {
            console.error('Error updating news:', error);
          }
        );
      } else {
        this.service.Post(formData).subscribe(
          (response) => {
            console.log(response);
            this.router.navigate(['/home']);
          },
          (error) => {
            console.error('Error posting news:', error);
          }
        );
      }
    } else {
      console.log('Form is invalid');
    }
  }
}
