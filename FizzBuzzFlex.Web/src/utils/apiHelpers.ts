import axios from 'axios';
import { GameMinimalDto, GameWriteDto } from '../models/dtos';
import { getReactQueryConfig } from './getReactQueryConfig';

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 10000,
});

export const createNewGame = async (game: GameWriteDto) => {
  await axiosClient.post('/v1/games', game);
};

export const GetAllGamesKey = 'getAllGames';
export const getAllGames = getReactQueryConfig(
  () => async () => {
    const response = await axiosClient.get<GameMinimalDto[]>('/v1/games/all');
    return response.data;
  },
  GetAllGamesKey
);
