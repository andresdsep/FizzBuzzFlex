import axios from 'axios';
import { GameWriteDto } from '../models/dtos';

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 10000,
});

export const createNewGame = async (game: GameWriteDto) => {
  await axiosClient.post('/games', game);
};
