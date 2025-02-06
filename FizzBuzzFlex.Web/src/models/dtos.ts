export interface GameWriteDto {
  id: string;
  name: string;
  author: string;
  divisorLabels: DivisorLabelWriteDto[];
}

export interface DivisorLabelWriteDto {
  id: string;
  divisor: string;
  label: string;
}
