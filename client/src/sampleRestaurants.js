import pizaSuzi from './picture/piza_suzi.png';
import asiti from './picture/asiti.jpg';
import cafeBat from './picture/cafe_bat.jpg';
const sampleRestaurants = [
    {
      id: 1,
      name: 'פיצה סושי',
      category: 'פיצה',
      location: 'תל אביב',
      image: pizaSuzi, // הקפד לשים תמונה קיימת
    },
    {
      id: 2,
      name: 'אסייתי-מטבח',
      category: 'אסייתי',
      location: 'תל אביב',
      image: asiti,
    },
    {
      id: 3,
      name: 'קפה בר',
      category: 'קפה',
      location: 'ירושלים',
      image: cafeBat,
    },
    // הוסף מסעדות נוספות כאן...
  ];
  export default sampleRestaurants;
  