import { Subject } from "rxjs";

export const profileSubject = new Subject<{ name: string; picUrl: string }>();
