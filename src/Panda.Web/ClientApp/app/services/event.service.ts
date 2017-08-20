import { Post } from "../models/post";
import { EventEmitter } from "@angular/core";

export class EventService {
    editPost = new EventEmitter<Post>();
}